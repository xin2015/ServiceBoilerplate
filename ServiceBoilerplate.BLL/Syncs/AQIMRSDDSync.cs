using Modules.AQE.AQI;
using ServiceBoilerplate.BLL.CNEMCService;
using ServiceBoilerplate.BLL.Extensions;
using ServiceBoilerplate.BLL.Syncs.Base;
using ServiceBoilerplate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs
{
    public class AQIMRSDDSync : AQIMSyncBase<AQIMRSDD>
    {
        public AQIMRSDDSync(OpenAccessContext context) : base(context)
        {
            Interval = TimeSpan.FromDays(1);
        }

        protected override IEnumerable<AQIMRSDD> GetSyncData(string code, DateTime time)
        {
            using (CNEMCServiceClient client = new CNEMCServiceClient())
            {
                string key = client.Login(GetLoginState());
                AQIDataPublishHistory[] srcList = client.GetAQIDataPublishHistoryListByTime(key, time.AddDays(1));
                if (srcList.Any())
                {
                    List<AQIMRSDD> list = new List<AQIMRSDD>();
                    DayAQICalculator calculator = new DayAQICalculator();
                    foreach (AQIDataPublishHistory src in srcList)
                    {
                        AQIMRSDD data = new AQIMRSDD()
                        {
                            Code = src.StationCode,
                            Time = time,
                            SO2 = src.SO2_24h.ToNullableDouble(),
                            NO2 = src.NO2_24h.ToNullableDouble(),
                            CO = src.CO_24h.ToNullableDouble(),
                            O3 = src.O3_8h.ToNullableDouble(),
                            PM10 = src.PM10_24h.ToNullableDouble(),
                            PM25 = src.PM2_5_24h.ToNullableDouble()
                        };
                        calculator.CalculateAQI(data);
                        list.Add(data);
                    }
                    list.Sort(Compare);
                    Rank(list);
                    return list;
                }
                else
                {
                    throw new Exception("获取数据失败！");
                }
            }
        }

        protected override DateTime GetTime()
        {
            return DateTime.Today.AddDays(-1);
        }
    }
}
