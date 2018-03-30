using ServiceBoilerplate.DAL.Entities.Base;
using ServiceBoilerplate.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.DAL.Repositories.Base
{
    public class Repository<TEntity> where TEntity : IEntity
    {
        protected OpenAccessContext Context { get; set; }

        public Repository(OpenAccessContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.GetAll<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            Context.Add(entities);
        }

        public virtual void AddBySqlBulkCopy(DataTable dt)
        {
            using (SqlBulkCopy sbc = new SqlBulkCopy(Context.Connection.ConnectionString))
            {
                sbc.DestinationTableName = dt.TableName;
                sbc.BatchSize = 30000;
                foreach (DataColumn item in dt.Columns)
                {
                    sbc.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                }
                sbc.WriteToServer(dt);
            }
        }

        public virtual void AddBySqlBulkCopy(IEnumerable<TEntity> entities)
        {
            DataTable dt = entities.GetDataTable();
            AddBySqlBulkCopy(dt);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Delete(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Context.Delete(entities);
        }
    }
}
