using Dapper;

public class AircraftMaintenanceRepository
{
    private readonly DapperContext _context;
    public AircraftMaintenanceRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AircraftMaintenance>> GetAllMaintenances()
    {
        var query = "SELECT * FROM aircraft_maintenance";
        using (var connection = _context.CreateConnection())
        {
            var maintenances = await connection.QueryAsync<AircraftMaintenance>(query);
            return maintenances.ToList();
        }
    }

    public async Task<AircraftMaintenance> GetMaintenanceById(int id)
    {
        var query = "SELECT * FROM aircraft_maintenance WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var maintenance = await connection.QuerySingleOrDefaultAsync<AircraftMaintenance>(query, new { Id = id });
            return maintenance;
        }
    }
}