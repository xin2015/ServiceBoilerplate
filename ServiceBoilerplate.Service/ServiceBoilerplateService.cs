using Common.Logging;
using Quartz;
using Quartz.Impl;
using ServiceBoilerplate.BLL.Syncs.Base;
using ServiceBoilerplate.Service.Jobs.Base;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Topshelf;

namespace ServiceBoilerplate.Service
{
    class ServiceBoilerplateService : ServiceControl
    {
        private ILog logger;
        private IScheduler scheduler;

        public ServiceBoilerplateService()
        {
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
                Type jobInterface = typeof(IJob);
                Type syncInterface = typeof(ISync);
                Type syncJobBaseType = typeof(SyncJobBase<>);
                Type coverJobBaseType = typeof(CoverJobBase<>);
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (type.Name.EndsWith("Job") && type.GetInterfaces().Contains(jobInterface))
                    {
                        scheduleJobAction(type.Name, type);
                    }
                }
                foreach (Type type in Assembly.GetAssembly(syncInterface).GetTypes())
                {
                    if (type.Name.EndsWith("Sync") && type.GetInterfaces().Contains(syncInterface))
                    {
                        string syncJobName = string.Format("{0}Job", type.Name);
                        string coverJobName = syncJobName.Replace("Sync", "Cover");
                        Type syncJobType = syncJobBaseType.MakeGenericType(type);
                        Type coverJobType = coverJobBaseType.MakeGenericType(type);
                        scheduleJobAction(syncJobName, syncJobType);
                        scheduleJobAction(coverJobName, coverJobType);
                    }
                }
                logger.Info("-------- Initialization Complete --------");
            }
            catch (Exception e)
            {
                logger.Fatal("Server initialization failed.", e);
            }
        }

        public virtual void Start()
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

        public virtual void Stop()
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

        public bool Start(HostControl hostControl)
        {
            Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Stop();
            return true;
        }
    }
}
