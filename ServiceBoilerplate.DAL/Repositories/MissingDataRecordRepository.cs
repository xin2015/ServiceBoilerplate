using ServiceBoilerplate.DAL.Entities;
using ServiceBoilerplate.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace ServiceBoilerplate.DAL.Repositories
{
    public class MissingDataRecordRepository : Repository<MissingDataRecord>
    {
        int maxMissTimes;
        public MissingDataRecordRepository(OpenAccessContext context) : base(context)
        {
            if (!int.TryParse(ConfigurationManager.AppSettings["MaxMissTimes"], out maxMissTimes))
            {
                maxMissTimes = 30;
            }
        }

        public MissingDataRecord FirstOrDefault(string entityName, string code, DateTime time)
        {
            return GetAll().FirstOrDefault(o => o.EntityName == entityName && o.Code == code && o.Time == time && !o.Status);
        }

        public List<MissingDataRecord> GetList(string entityName)
        {
            return GetAll().Where(o => o.EntityName == entityName && !o.Status && o.MissTimes <= maxMissTimes).ToList();
        }
    }
}
