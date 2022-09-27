using Cliente.Shared.DataTransferObjects;
using MediatR;

namespace Cliente.Application.Queries
{
    public sealed record GetClienteQuery(int Id, bool TrackChanges) : IRequest<ClienteDto>;
}
