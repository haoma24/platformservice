using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PlatformService.CQRS.Platform.CreatePlatform
{
    public record CreatePlatformCommand (string Name, string Publisher, string Cost) : IRequest<int>;
}
