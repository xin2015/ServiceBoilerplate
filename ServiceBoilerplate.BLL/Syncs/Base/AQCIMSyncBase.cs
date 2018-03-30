using Modules.AQE.AQCI;
using ServiceBoilerplate.DAL.Entities;
using ServiceBoilerplate.DAL.Entities.Base;
using ServiceBoilerplate.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs.Base
{
    public abstract class AQCIMSyncBase<TEntity> : SingleThreadSyncBase<TEntity>, ISync where TEntity : ICodePeriodicRankEntity, IAQCIResult
    {
        protected static int Compare(TEntity x, TEntity y)
        {
            if (x.AQCI == null)
            {
                if (y.AQCI == null)
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
                if (y.AQCI == null)
                {
                    return -1;
                }
                else
                {
                    return x.AQCI.Value.CompareTo(y.AQCI.Value);
                }
            }
        }

        protected static void Rank(List<TEntity> list)
        {
            double? aqci = list.First().AQCI;
            int rank = 1, order = 1;
            list.ForEach(o =>
            {
                if (o.AQCI == null)
                {
                    o.Rank = order;
                }
                else
                {
                    if (o.AQCI > aqci)
                    {
                        aqci = o.AQCI;
                        rank = order;
                    }
                    o.Rank = rank;
                    order++;
                }
            });
        }

        protected Repository<AQIMRCDD> AQIMRCDDRepository;

        public AQCIMSyncBase(OpenAccessContext context) : base(context)
        {
            Interval = TimeSpan.FromDays(1);
            AQIMRCDDRepository = new Repository<AQIMRCDD>(context);
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

        protected override DateTime GetTime()
        {
            return DateTime.Today.AddDays(-1);
        }
        #endregion
    }
}
