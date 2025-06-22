using MediatR;
using PlatformService.Dtos;

namespace PlatformService.CQRS.Platform.GetPlatformById
{
    public record GetPlatformByIdQuery(int Id) : IRequest<PlatformReadDto>
    {
    }
}
