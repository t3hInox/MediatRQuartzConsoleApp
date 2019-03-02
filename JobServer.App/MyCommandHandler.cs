using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobServer.App
{
    public class MyCommandHandler : IRequestHandler<MyCommand, bool>
    {
        public async Task<bool> Handle(MyCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Success!");
            return true;
        }
    }
}
