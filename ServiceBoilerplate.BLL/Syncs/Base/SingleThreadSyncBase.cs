using ServiceBoilerplate.DAL.Entities;
using ServiceBoilerplate.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs.Base
{
    public abstract class SingleThreadSyncBase<TEntity> : SyncBase<TEntity>, ISync where TEntity : ICodePeriodicEntity
    {
        protected virtual List<TEntity> List { get; set; }
        protected virtual List<MissingDataRecord> MDRList { get; set; }

        public SingleThreadSyncBase(OpenAccessContext context) : base(context)
        {
            List = new List<TEntity>();
            MDRList = new List<MissingDataRecord>();
        }

        #region 私有方法
        protected override void AddEntity(IEnumerable<TEntity> entities)
        {
            List.AddRange(entities);
        }

        protected override void AddMissingDataRecord(MissingDataRecord record)
        {
            MDRList.Add(record);
        }

        protected override void Add()
        {
            AddEntities(List);
            List.Clear();
            AddMissingDataRecords(MDRList);
            MDRList.Clear();
        }
        #endregion
    }
}
