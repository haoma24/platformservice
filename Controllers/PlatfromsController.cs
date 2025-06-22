using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PlatformService.AsyncDataServices;
using PlatformService.CQRS.Platform.CreatePlatform;
using PlatformService.CQRS.Platform.GetPlatformById;
using PlatformService.Data;
using PlatformService.Data.Caching;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IRedisCachingService? _cache;
        private readonly ISender _sender;

        public PlatformsController(IPlatformRepo repository,
        IMapper mapper,
        ICommandDataClient commandDataClient,
        IMessageBusClient messageBusClient,
        IRedisCachingService? cache,
        ISender sender
        )
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
            _cache = cache;
            _sender = sender;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from PlatformService");
            var userId = Request.Headers["UserId"];
            var cachingKey = $"platforms_{userId}";
            var platforms = _cache.GetData<IEnumerable<PlatformReadDto>>(cachingKey);
            if (platforms is not null)
                return Ok(platforms);
            var platformItem = _repository.GetAllPlatforms();
            _cache.SetData(cachingKey, platformItem);
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            var platformItem = await _sender.Send(new GetPlatformByIdQuery(id));
            if (platformItem != null)
            {
                return Ok(platformItem);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreatePlatform(CreatePlatformCommand command)
        {
            var platformId = await _sender.Send(command);
            return Ok(platformId);
            //var platformModel = _mapper.Map<Platform>(command);
            //_repository.CreatePlatform(platformModel);
            //_repository.SaveChanges();

            //var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            // Send Sync Message
            //try
            //{
            //    await _commandDataClient.SendPlatformToCommand(platformReadDto);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"--. Could not send synchronously: {ex.Message}");
            //}

            // Send Async Message
            //try
            //{
            //    var platformPublishDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
            //    platformPublishDto.Event = "Platform_Published";
            //    _messageBusClient.PublishNewPlatform(platformPublishDto);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"--. Could not send asynchronously: {ex.Message}");
            //}
            //return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}