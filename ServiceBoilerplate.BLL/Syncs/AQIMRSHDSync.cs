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
    public class AQIMRSHDSync : AQIMSyncBase<AQIMRSHD>
    {
        public AQIMRSHDSync(OpenAccessContext context) : base(context)
        {
            Interval = TimeSpan.FromHours(1);
        }

        protected override IEnumerable<AQIMRSHD> GetSyncData(string code, DateTime time)
        {
            using (CNEMCServiceClient client = new CNEMCServiceClient())
            {
                string key = client.Login(GetLoginState());
                AQIDataPublishHistory[] srcList = client.GetAQIDataPublishHistoryListByTime(key, time);
                if (srcList.Any())
                {
                    List<AQIMRSHD> list = new List<AQIMRSHD>();
                    HourAQICalculator calculator = new HourAQICalculator();
                    foreach (AQIDataPublishHistory src in srcList)
                    {
                        AQIMRSHD data = new AQIMRSHD()
                        {
                            Code = src.StationCode,
                            Time = time,
                            SO2 = src.SO2.ToNullableDouble(),
                            NO2 = src.NO2.ToNullableDouble(),
                            CO = src.CO.ToNullableDouble(),
                            O3 = src.O3_24h.ToNullableDouble(),
                            PM10 = src.PM10.ToNullableDouble(),
                            PM25 = src.PM2_5.ToNullableDouble()
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
            return DateTime.Today.AddHours(DateTime.Now.Hour);
        }
    }
}
