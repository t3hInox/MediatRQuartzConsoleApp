using JobServer.App;
using MediatR;
using Quartz;
using System;
using System.Threading.Tasks;

namespace JobServer.Jobs
{
    [DisallowConcurrentExecution]
    public class MyJob : IJob
    {
        private readonly IMediator _mediator;

        public MyJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine($"Executed: {DateTime.Now}");
                await _mediator.Send(new MyCommand());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
