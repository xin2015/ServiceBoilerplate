using Common.Logging;
using Polly;
using ServiceBoilerplate.DAL.Entities;
using ServiceBoilerplate.DAL.Entities.Base;
using ServiceBoilerplate.DAL.Repositories;
using ServiceBoilerplate.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.BLL.Syncs.Base
{
    public interface ISync
    {
        /// <summary>
        /// 同步数据
        /// </summary>
        void Sync();

        /// <summary>
        /// 回补数据
        /// </summary>
        void Cover();

        /// <summary>
        /// 回补数据
        /// </summary>
        /// <param name="list">回补记录集合</param>
        void Cover(IEnumerable<MissingDataRecord> list);

        /// <summary>
        /// 回补数据
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        void Cover(DateTime beginTime, DateTime endTime);
    }

    public abstract class SyncBase<TEntity> : ISync where TEntity : ICodePeriodicEntity
    {
        protected Repository<TEntity> Repository { get; set; }
        protected MissingDataRecordRepository MDRRepository { get; set; }
        protected ILog Logger { get; set; }
        protected string EntityName { get; set; }
        protected TimeSpan Interval { get; set; }

        public SyncBase(OpenAccessContext context)
        {
            Repository = new Repository<TEntity>(context);
            MDRRepository = new MissingDataRecordRepository(context);
            EntityName = typeof(TEntity).Name;
            Logger = LogManager.GetLogger<TEntity>();
        }

        #region 私有方法
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        protected abstract DateTime GetTime();

        /// <summary>
        /// 获取时间点对应的所有编码
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        protected abstract string[] GetCodes(DateTime time);

        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="time">时间</param>
        protected virtual void Sync(string code, DateTime time)
        {
            try
            {
                Policy policy = Policy.Handle<Exception>().WaitAndRetry(1, retryAttempt => TimeSpan.FromMinutes(retryAttempt * 1), (exception, timeSpan) =>
                  {
                      Logger.ErrorFormat("Polly Retry: {0}, {1}, {2}", exception, EntityName, code, time);
                  });
                policy.Execute(() => AddEntity(GetSyncData(code, time)));
            }
            catch (Exception e)
            {
                MissingDataRecord record = new MissingDataRecord()
                {
                    EntityName = EntityName,
                    Code = code,
                    Time = time,
                    Exception = e.Message
                };
                AddMissingDataRecord(record);
            }
        }

        /// <summary>
        /// 根据编码获取数据（同步）
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> GetSyncData(string code, DateTime time);

        /// <summary>
        /// 根据编码获取数据（回补）
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        protected virtual IEnumerable<TEntity> GetCoverData(string code, DateTime time)
        {
            return GetSyncData(code, time);
        }

        /// <summary>
        /// 添加数据到Sync
        /// </summary>
        /// <param name="entities">数据</param>
        protected abstract void AddEntity(IEnumerable<TEntity> entities);

        /// <summary>
        /// 添加回补记录到Sync
        /// </summary>
        /// <param name="record">回补记录</param>
        protected abstract void AddMissingDataRecord(MissingDataRecord record);

        /// <summary>
        /// 添加数据和回补记录到数据库，并清理Sync中的数据和回补记录
        /// </summary>
        protected abstract void Add();

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="entities"></param>
        protected virtual void AddEntities(IEnumerable<TEntity> entities)
        {
            Repository.Add(entities);
        }

        /// <summary>
        /// 添加回补记录到数据库
        /// </summary>
        /// <param name="records"></param>
        protected virtual void AddMissingDataRecords(IEnumerable<MissingDataRecord> records)
        {
            MDRRepository.Add(records);
        }

        /// <summary>
        /// 获取回补记录
        /// </summary>
        /// <returns></returns>
        protected virtual List<MissingDataRecord> GetRecordsForCover()
        {
            return MDRRepository.GetList(EntityName);
        }

        /// <summary>
        /// 回补数据
        /// </summary>
        /// <param name="mdr">回补记录</param>
        protected virtual void Cover(MissingDataRecord mdr)
        {
            try
            {
                AddEntity(GetCoverData(mdr.Code, mdr.Time));
                mdr.Status = true;
            }
            catch (Exception e)
            {
                mdr.Exception = e.Message;
                mdr.MissTimes += 1;
            }
        }

        /// <summary>
        /// 判断是否已同步
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        protected abstract bool IsSynchronized(string code, DateTime time);
        #endregion

        #region 公共方法
        /// <summary>
        /// 同步数据
        /// </summary>
        public virtual void Sync()
        {
            DateTime time = GetTime();
            try
            {
                string[] codes = GetCodes(time);
                foreach (string code in codes)
                {
                    Sync(code, time);
                }
                Add();
            }
            catch (Exception e)
            {
                Logger.Error("Sync failed.", e);
            }
        }

        /// <summary>
        /// 回补数据
        /// </summary>
        public virtual void Cover()
        {
            List<MissingDataRecord> mdrList = GetRecordsForCover();
            Cover(mdrList);
        }

        /// <summary>
        /// 回补数据
        /// </summary>
        /// <param name="list">回补记录集合</param>
        public virtual void Cover(IEnumerable<MissingDataRecord> list)
        {
            try
            {
                foreach (MissingDataRecord mdr in list)
                {
                    Cover(mdr);
                }
                Add();
            }
            catch (Exception e)
            {
                Logger.Error("Cover failed.", e);
            }
        }

        /// <summary>
        /// 回补数据
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public virtual void Cover(DateTime beginTime, DateTime endTime)
        {
            try
            {
                for (DateTime time = beginTime; time <= endTime; time = time.Add(Interval))
                {
                    string[] codes = GetCodes(time);
                    foreach (string code in codes)
                    {
                        if (!IsSynchronized(code, time))
                        {
                            MissingDataRecord mdr = MDRRepository.FirstOrDefault(EntityName, code, time);
                            if (mdr == null)
                            {
                                Sync(code, time);
                            }
                            else
                            {
                                Cover(mdr);
                            }
                        }
                    }
                }
                Add();
            }
            catch (Exception e)
            {
                Logger.Error("Cover failed.", e);
            }
        }
        #endregion
    }
}
