using Quartz;
using Quartz.Impl;
using SampleJobQuartzNet.Jobs;
using System;
using System.Threading.Tasks;

namespace SampleJobQuartzNet
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("HelloWorld", "Hello").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(2)
                .WithRepeatCount(5))
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await Task.Delay(TimeSpan.FromSeconds(20));
            await scheduler.Shutdown();
        }
    }
}
