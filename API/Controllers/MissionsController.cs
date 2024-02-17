using AutoMapper;
using Business.Services.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IMapper _mapper;

        public MissionsController(IMissionService missionService, IMapper mapper)
        {
            _mapper = mapper;
            _missionService = missionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var missions = await _missionService.GetAllAsync();
            var missionsDto = _mapper.Map<List<MissionDto>>(missions.ToList());
            return Ok(missionsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mission = await _missionService.GetByIdAsync(id);
            var missionDto = _mapper.Map<MissionDto>(mission);
            return Ok(missionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Save(MissionDto missionDto)
        {
            var mission = _mapper.Map<Mission>(missionDto);
            await _missionService.AddAsync(mission);
            return CreatedAtAction(nameof(GetById), new { id = mission.Id }, mission);

        }

        [HttpPut]
        public async Task<IActionResult> Update(MissionDto missionDto)
        {
            var mission = _mapper.Map<Mission>(missionDto);
            await _missionService.UpdateAsync(mission);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var mission = await _missionService.GetByIdAsync(id);
            await _missionService.RemoveAsync(mission);
            return NoContent();
        }
    }
}
