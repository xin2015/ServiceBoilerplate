using ServiceBoilerplate.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.DAL.Entities
{
    /// <summary>
    /// 数据缺失记录
    /// </summary>
    public class MissingDataRecord : ICodePeriodicEntity, ICreationEntity, ITimestamp
    {
        public string EntityName { get; set; }

        public string Code { get; set; }

        public DateTime Time { get; set; }

        public bool Status { get; set; }

        public int MissTimes { get; set; }

        public string Exception { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
