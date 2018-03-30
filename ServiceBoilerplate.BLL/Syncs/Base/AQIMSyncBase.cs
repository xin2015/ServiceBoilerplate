using Modules.AQE.AQI;
using Modules.Basic.CryptoTransverters;
using Newtonsoft.Json;
using ServiceBoilerplate.BLL.CNEMCService;
using ServiceBoilerplate.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs.Base
{
    public abstract class AQIMSyncBase<TEntity> : SingleThreadSyncBase<TEntity>, ISync where TEntity : ICodePeriodicRankEntity, IAQIResult
    {
        protected static int Compare(TEntity x, TEntity y)
        {
            if (x.AQI == null)
            {
                if (y.AQI == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (y.AQI == null)
                {
                    return -1;
                }
                else
                {
                    return x.AQI.Value.CompareTo(y.AQI.Value);
                }
            }
        }

        protected static void Rank(List<TEntity> list)
        {
            double? aqi = list.First().AQI;
            int rank = 1, order = 1;
            list.ForEach(o =>
            {
                if (o.AQI == null)
                {
                    o.Rank = order;
                }
                else
                {
                    if (o.AQI > aqi)
                    {
                        aqi = o.AQI;
                        rank = order;
                    }
                    o.Rank = rank;
                    order++;
                }
            });
        }

        public AQIMSyncBase(OpenAccessContext context) : base(context)
        {
        }

        #region 私有方法
        protected override string[] GetCodes(DateTime time)
        {
            return new string[] { string.Empty };
        }

        protected override void AddEntities(IEnumerable<TEntity> entities)
        {
            Repository.AddBySqlBulkCopy(entities);
        }

        protected override bool IsSynchronized(string code, DateTime time)
        {
            return Repository.GetAll().Any(o => o.Time == time);
        }

        protected static string GetLoginState()
        {
            LoginUser user = new LoginUser()
            {
                UserName = ConfigurationManager.AppSettings["UserName"],
                Password = ConfigurationManager.AppSettings["Password"],
                LoginTime = DateTimeOffset.Now
            };
            string jsonData = JsonConvert.SerializeObject(user);
            string state = SymmetricalEncryption.Default.Encrypt(jsonData);
            return state;
        }
        #endregion
    }
}
