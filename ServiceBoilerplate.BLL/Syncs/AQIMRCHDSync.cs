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
    public class AQIMRCHDSync : AQIMSyncBase<AQIMRCHD>
    {
        public AQIMRCHDSync(OpenAccessContext context) : base(context)
        {
            Interval = TimeSpan.FromHours(1);
        }

        protected override IEnumerable<AQIMRCHD> GetSyncData(string code, DateTime time)
        {
            using (CNEMCServiceClient client = new CNEMCServiceClient())
            {
                string key = client.Login(GetLoginState());
                CityAQIPublishHistory[] srcList = client.GetCityAQIPublishHistoryListByTime(key, time);
                if (srcList.Any())
                {
                    List<AQIMRCHD> list = new List<AQIMRCHD>();
                    HourAQICalculator calculator = new HourAQICalculator();
                    foreach (CityAQIPublishHistory src in srcList)
                    {
                        AQIMRCHD data = new AQIMRCHD()
                        {
                            Code = src.CityCode.ToString(),
                            Time = time,
                            SO2 = src.SO2.ToNullableDouble(),
                            NO2 = src.NO2.ToNullableDouble(),
                            CO = src.CO.ToNullableDouble(),
                            O3 = src.O3.ToNullableDouble(),
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
