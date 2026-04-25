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
}