using AutoMapper;
using MediatR;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.CQRS.Platform.CreatePlatform
{
    public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CreatePlatformCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<int> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
        {
            var platform = _mapper.Map<PlatformService.Models.Platform>(request);
            _context.Platforms.Add(platform);
            await _context.SaveChangesAsync();
            return platform.Id;
        }
    }
}
