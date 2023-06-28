using AutoMapper;
using Microservices.PlatformService.AsyncDataService;
using Microservices.PlatformService.Data.Abstracts;
using Microservices.PlatformService.Models.DTO;
using Microservices.PlatformService.Models.EntityData;
using Microservices.PlatformService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private IPlatformservice _service;
        private IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
                 IPlatformservice service,
                 IMapper mapper,
                 ICommandDataClient commandDataClient,
                 IMessageBusClient messageBusClient)
        {
            _service = service;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<List<PlatformReadDTO>> GetPlatform()
        {
            Console.WriteLine("--> Getting Platforms....");
            return Ok(_mapper.Map<List<PlatformReadDTO>>(_service.GetAllPlatforms()));
        }

        [HttpGet("{Id}")]
        public ActionResult<PlatformReadDTO> GetPlatFormById(int Id)
        {
            return Ok(_mapper.Map<PlatformReadDTO>(_service.GetPlatformById(Id)));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platform)
        {
            var platformModel = _mapper.Map<Platform>(platform);
            var newPlatform = _service.CreatePlatform(platformModel);
            var platformReadDTO = _mapper.Map<PlatformReadDTO>(platformModel);
            if (_service.SaveChanges())
            {
                //Sync Message
                try
                {
                    await _commandDataClient.SendPlatformToCommand(platformReadDTO);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
                }

                //Async Message
                try
                {
                    var platformPublishedDto = _mapper.Map<PlatformPublishedDTO>(platformReadDTO);
                    platformPublishedDto.Event = "Platform_Published";
                    _messageBusClient.PublishNewPlatform(platformPublishedDto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
                }

                return Ok(newPlatform);
            }
            else
                return BadRequest();

        }
    }
}
