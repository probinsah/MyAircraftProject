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
        var query = @"SELECT 
                    id,
                    aircraft_code AS ""AircraftCode"",
                    aircraft_model AS ""AircraftModel"",
                    maintenance_type AS ""MaintenanceType"",
                    description,
                    maintenance_date AS ""MaintenanceDate"",
                    next_due_date AS ""NextDueDate"",
                    engineer_name AS ""EngineerName"",
                    status,
                    created_at AS ""CreatedAt""
                    FROM ua.aircraft_maintenance
                    ORDER BY id";
        using (var connection = _context.CreateConnection())
        {
            var maintenances = await connection.QueryAsync<AircraftMaintenance>(query);
            return maintenances.ToList();
        }
    }

    public async Task<AircraftMaintenance> GetMaintenanceById(int id)
    {
        var query = @"SELECT 
                    id,
                    aircraft_code AS ""AircraftCode"",
                    aircraft_model AS ""AircraftModel"",
                    maintenance_type AS ""MaintenanceType"",
                    description,
                    maintenance_date AS ""MaintenanceDate"",
                    next_due_date AS ""NextDueDate"",
                    engineer_name AS ""EngineerName"",
                    status,
                    created_at AS ""CreatedAt""
                    FROM ua.aircraft_maintenance
                    WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var maintenance = await connection.QuerySingleOrDefaultAsync<AircraftMaintenance>(query, new { Id = id });
            return maintenance;
        }
    }
    public async Task<IEnumerable<AircraftMaintenance>> GetByStatusAsync(string status)
    {
        var query = "SELECT * FROM ua.get_maintenance_by_status(@Status)";
        using (var connection = _context.CreateConnection())        {
            var maintenances = await connection.QueryAsync<AircraftMaintenance>(query, new { Status = status });
            return maintenances.ToList();
        }
    }
    public async Task InsertMaintenanceAsync(AircraftMaintenance maintenance)
    {
        var query = @"CALL ua.insert_maintenance(
                    @AircraftCode,
                    @AircraftModel,
                    @MaintenanceType,
                    @Description,
                    @MaintenanceDate,
                    @NextDueDate,
                    @EngineerName,
                    @Status
                )";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, maintenance);
        }
    }
    public async Task UpdateMaintenanceStatusAsync(int id, string status)
    {
        var query = @"UPDATE ua.aircraft_maintenance SET status = @Status WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { Id = id, Status = status });
        }
    }
    public async Task DeleteMaintenanceAsync(int id)
    {
        var query = @"DELETE FROM ua.aircraft_maintenance WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}