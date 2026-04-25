using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AircraftMaintenanceController : ControllerBase
{
    private readonly AircraftMaintenanceRepository _repository;

    public AircraftMaintenanceController(AircraftMaintenanceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftMaintenance>>> GetAllMaintenances()
    {
        var maintenances = await _repository.GetAllMaintenances();
        return Ok(maintenances);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AircraftMaintenance>> GetMaintenanceById(int id)
    {
        var maintenance = await _repository.GetMaintenanceById(id);
        if (maintenance == null)
            return NotFound();

        return Ok(maintenance);
    }
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<AircraftMaintenance>>> GetMaintenancesByStatus(string status)
    {
        var maintenances = await _repository.GetByStatusAsync(status);
        return Ok(maintenances);
    }
    [HttpPost]
    public async Task<ActionResult> InsertMaintenance(AircraftMaintenance maintenance)
    {
        await _repository.InsertMaintenanceAsync(maintenance);
        return Ok();
    }
}