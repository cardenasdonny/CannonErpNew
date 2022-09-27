using MediatR;

namespace Cliente.Application.Commands
{    
    public record ClienteCreateCommand(string Nombre) : INotification;

}
