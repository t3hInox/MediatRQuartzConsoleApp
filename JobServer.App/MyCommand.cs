using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobServer.App
{
    public class MyCommand : IRequest<bool>
    {
    }
}
