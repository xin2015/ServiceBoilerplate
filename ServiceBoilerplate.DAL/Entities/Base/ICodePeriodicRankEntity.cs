using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.DAL.Entities.Base
{
    public interface ICodePeriodicRankEntity : ICodePeriodicEntity
    {
        int Rank { get; set; }
    }
}
