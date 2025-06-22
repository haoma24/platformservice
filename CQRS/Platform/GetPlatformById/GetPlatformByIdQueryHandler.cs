using AutoMapper;
using MediatR;
using PlatformService.Data;
using PlatformService.Dtos;

namespace PlatformService.CQRS.Platform.GetPlatformById
{
    
    public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, PlatformReadDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GetPlatformByIdQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PlatformReadDto> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
        {
            var platform = await _context.Platforms.FindAsync(request.Id);
            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
            return platformReadDto;
        }
    }
}
