using ServiceBoilerplate.DAL.Entities;
using ServiceBoilerplate.DAL.Entities.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs.Base
{
    public abstract class MultithreadingSyncBase<TEntity> : SyncBase<TEntity>, ISync where TEntity : ICodePeriodicEntity
    {
        protected virtual ConcurrentQueue<TEntity> Queue { get; set; }
        protected virtual ConcurrentQueue<MissingDataRecord> MDRQueue { get; set; }
        protected virtual List<Task> TaskList { get; set; }

        public MultithreadingSyncBase(OpenAccessContext context) : base(context)
        {
            Queue = new ConcurrentQueue<TEntity>();
            MDRQueue = new ConcurrentQueue<MissingDataRecord>();
            TaskList = new List<Task>();
        }

        #region 私有方法
        protected override void Sync(string code, DateTime time)
        {
            TaskList.Add(Task.Run(() =>
            {
                base.Sync(code, time);
            }));
        }

        protected override void AddEntity(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Queue.Enqueue(entity);
            }
        }

        protected override void AddMissingDataRecord(MissingDataRecord record)
        {
            MDRQueue.Enqueue(record);
        }

        protected override void Add()
        {
            Task.WaitAll(TaskList.ToArray());
            TaskList.Clear();
            if (Queue.Count > 0)
            {
                List<TEntity> list = new List<TEntity>();
                TEntity entity;
                while (Queue.TryDequeue(out entity))
                {
                    list.Add(entity);
                }
                AddEntities(list);
            }
            if (MDRQueue.Count > 0)
            {
                List<MissingDataRecord> list = new List<MissingDataRecord>();
                MissingDataRecord record;
                while (MDRQueue.TryDequeue(out record))
                {
                    list.Add(record);
                }
                AddMissingDataRecords(list);
            }
        }

        protected override void Cover(MissingDataRecord mdr)
        {
            TaskList.Add(Task.Run(() =>
            {
                base.Cover(mdr);
            }));
        }
        #endregion
    }
}
