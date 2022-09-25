using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Commands
{
    public class ClienteCreateCommand : INotification
    {
        public string Nombre { get; set; }
    }
}
