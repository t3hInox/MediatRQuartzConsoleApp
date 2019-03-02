using JobServer;
using JobServer.App;
using JobServer.Jobs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz.DependencyInjection.Microsoft.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRQuartzConsoleApp
{
    public class Program
    {
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("Job Server started");

            //DI setup
            var serviceProvider = new ServiceCollection()
                .AddQuartz()
                .AddTransient<InitializeJobServer>()
                .AddTransient<MyJob>()
                .AddTransient<MyCommand>()
                .AddTransient<MyCommandHandler>()
                .AddMediatR(typeof(MyCommand).GetType().Assembly)
                .BuildServiceProvider();

            Console.CancelKeyPress += (sender, eArgs) => {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            Task.Run(async () =>
            {
                try
                {
                    await serviceProvider.GetService<InitializeJobServer>().Start();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            _quitEvent.WaitOne();
            Console.WriteLine("Job Server ended");
        }
    }
}
