using JobServer.Jobs;
using Quartz;
using System;
using System.Threading.Tasks;

namespace JobServer
{
    public class InitializeJobServer
    {
        private IScheduler _scheduler;
        private readonly ISchedulerFactory _schedulerFactory;

        public InitializeJobServer(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task Start()
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler();
                await _scheduler.Start();

                IJobDetail job = JobBuilder.Create<MyJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                await _scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        ~InitializeJobServer()
        {
            _scheduler.Shutdown();
        }
    }
}
