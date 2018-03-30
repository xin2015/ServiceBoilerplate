using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.DAL.Entities.Base
{
    public interface ITimestamp
    {
        DateTime Timestamp { get; set; }
    }
}
