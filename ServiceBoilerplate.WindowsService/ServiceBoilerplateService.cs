using Common.Logging;
using Quartz;
using Quartz.Impl;
using ServiceBoilerplate.BLL.Syncs.Base;
using ServiceBoilerplate.WindowsService.Jobs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.WindowsService
{
    public partial class ServiceBoilerplateService : ServiceBase
    {
        private ILog logger;
        private IScheduler scheduler;

        public ServiceBoilerplateService()
        {
            InitializeComponent();
            logger = LogManager.GetLogger<ServiceBoilerplateService>();
            Initialize();
        }

        public virtual void Initialize()
        {
            try
            {
                logger.Info("-------- Initialization Start --------");
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                logger.Info("-------- Scheduling Jobs --------");
                Action<string, Type> scheduleJobAction = (jobName, jobType) =>
                {
                    IJobDetail job = JobBuilder.Create(jobType)
                        .WithIdentity(jobName)
                        .Build();
                    ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                        .WithIdentity(string.Format("Trigger{0}", jobName))
                        .WithCronSchedule(ConfigurationManager.AppSettings[string.Format("{0}CronExpression", jobName)])
                        .Build();
                    scheduler.ScheduleJob(job, trigger);
                };
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                Type jobInterface = typeof(IJob);
                Type syncInterface = typeof(ISync);
                Type syncJobBaseType = typeof(SyncJobBase<>);
                Type coverJobBaseType = typeof(CoverJobBase<>);
                foreach (Type type in types)
                {
                    if (type.Name.EndsWith("Job"))
                    {
                        if (type.GetInterfaces().Contains(jobInterface))
                        {
                            scheduleJobAction(type.Name, type);
                        }
                    }
                    else if (type.Name.EndsWith("Sync"))
                    {
                        if (type.GetInterfaces().Contains(syncInterface))
                        {
                            string syncJobName = string.Format("{0}Job", type.Name);
                            string coverJobName = syncJobName.Replace("Sync", "Cover");
                            Type syncJobType = syncJobBaseType.MakeGenericType(type);
                            Type coverJobType = coverJobBaseType.MakeGenericType(type);
                            scheduleJobAction(syncJobName, syncJobType);
                            scheduleJobAction(coverJobName, coverJobType);
                        }
                    }
                }
                logger.Info("-------- Initialization Complete --------");
            }
            catch (Exception e)
            {
                logger.Fatal("Server initialization failed.", e);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                scheduler.Start();
            }
            catch (Exception ex)
            {
                logger.Fatal("Scheduler start failed.", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                logger.Error("Scheduler stop failed.", ex);
            }
        }
    }
}
