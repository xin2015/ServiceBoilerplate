using Modules.AQE.AQCI;
using ServiceBoilerplate.BLL.Extensions;
using ServiceBoilerplate.BLL.Syncs.Base;
using ServiceBoilerplate.DAL.Entities;
using ServiceBoilerplate.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs
{
    public class AQCIMRCSDSync : AQCIMSyncBase<AQCIMRCSD>
    {
        public AQCIMRCSDSync(OpenAccessContext context) : base(context) { }

        protected override IEnumerable<AQCIMRCSD> GetSyncData(string code, DateTime time)
        {
            DateTime beginTime = new DateTime(time.Year, (time.Month - 1) / 3 * 3 + 1, 1);   //季度的第一天
            List<AQIMRCDD> srcList = AQIMRCDDRepository.GetAll().Where(o => o.Time >= beginTime && o.Time <= time).ToList();
            if (srcList.Any())
            {
                List<AQCIMRCSD> list = new List<AQCIMRCSD>();
                AQCICalculator calculator = new AQCICalculator();
                foreach (var codeGroup in srcList.GroupBy(o => o.Code))
                {
                    AQCIMRCSD data = new AQCIMRCSD()
                    {
                        Code = codeGroup.Key,
                        Time = time,
                        SO2 = codeGroup.Average(o => o.SO2).Round(),
                        NO2 = codeGroup.Average(o => o.NO2).Round(),
                        PM10 = codeGroup.Average(o => o.PM10).Round(),
                        PM25 = codeGroup.Average(o => o.PM25).Round()
                    };
                    double[] array = codeGroup.Where(o => o.CO.HasValue).Select(o => o.CO.Value).ToArray();
                    if (array.Length == 1)
                    {
                        data.CO = array.First();
                    }
                    else if (array.Length != 0)
                    {
                        data.CO = Math.Round(calculator.CalculatePercentile(array, 0.95), 1);
                    }
                    array = codeGroup.Where(o => o.O3.HasValue).Select(o => o.O3.Value).ToArray();
                    if (array.Length == 1)
                    {
                        data.O3 = array.First();
                    }
                    else if (array.Length != 0)
                    {
                        data.O3 = Math.Round(calculator.CalculatePercentile(array, 0.9));
                    }
                    calculator.CalculateAQCI(data);
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
}
