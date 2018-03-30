using Common.Logging;
using Quartz;
using ServiceBoilerplate.BLL.Syncs.Base;
using ServiceBoilerplate.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.Service.Jobs.Base
{
    class CoverJobBase<TSync> : IJob where TSync : ISync
    {
        ILog logger;

        public CoverJobBase()
        {
            logger = LogManager.GetLogger<CoverJobBase<TSync>>();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using (ServiceBoilerplateContext db = new ServiceBoilerplateContext())
                {
                    ISync sync = (ISync)Activator.CreateInstance(typeof(TSync), db);
                    sync.Cover();
                    db.SaveChanges();
                }
                sw.Stop();
                logger.InfoFormat("{0} Cover {1}.", typeof(TSync).Name, sw.Elapsed);
            }
            catch (Exception e)
            {
                logger.Error("Execute failed.", e);
            }
        }
    }
}
