using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AircraftMaintenanceController : ControllerBase
{
    private readonly AircraftMaintenanceRepository _repository;
    private readonly IMapper _mapper;

    public AircraftMaintenanceController(AircraftMaintenanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftMaintenanceDto>>> GetAllMaintenances()
    {
        var maintenances = await _repository.GetAllMaintenances();
        var result = _mapper.Map<IEnumerable<AircraftMaintenanceDto>>(maintenances);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AircraftMaintenanceDto>> GetMaintenanceById(int id)
    {
        var maintenance = await _repository.GetMaintenanceById(id);
        if (maintenance == null)
            return NotFound();

        var result = _mapper.Map<AircraftMaintenanceDto>(maintenance);
        return Ok(result);
    }
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<AircraftMaintenanceDto>>> GetMaintenancesByStatus(string status)
    {
        var maintenances = await _repository.GetByStatusAsync(status);
        var result = _mapper.Map<IEnumerable<AircraftMaintenanceDto>>(maintenances);
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult> InsertMaintenance(AircraftMaintenance maintenance)
    {
        await _repository.InsertMaintenanceAsync(maintenance);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMaintenanceStatus(int id, string status)
    {
        await _repository.UpdateMaintenanceStatusAsync(id, status);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMaintenance(int id)
    {
        await _repository.DeleteMaintenanceAsync(id);
        return Ok();
    }
}