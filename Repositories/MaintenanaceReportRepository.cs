using Dapper;

public class MaintenanceReportRepository
{
    private readonly DapperContext _context;

    public MaintenanceReportRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceReport>> GetAllReports()
    {
        var query = @"SELECT
                        id,
                        aircraft_code AS ""AircraftCode"",
                        report_type AS ""ReportType"",
                        status AS ""Status"",
                        report_data AS ""ReportData"",
                        created_by AS ""CreatedBy"",
                        created_at AS ""CreatedAt""
                      FROM ua.maintenance_reports ORDER BY id";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<MaintenanceReport>(query);
    }

    public async Task<MaintenanceReport> GetReportById(int id)
    {
        var query = @"SELECT
                        id AS Id,
                        aircraft_code AS ""AircraftCode"",
                        report_type AS ""ReportType"",
                        status AS ""Status"",
                        report_data AS ""ReportData"",
                        created_by AS ""CreatedBy"",
                        created_at AS ""CreatedAt""
                      FROM ua.maintenance_reports WHERE id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<MaintenanceReport>(query, new { Id = id });
    }

    public async Task<int> InsertReportAsync(MaintenanceReport report)
    {
        var query = @"INSERT INTO ua.maintenance_reports (aircraft_code, report_type, status, report_data, created_by, created_at) 
                      VALUES (@AircraftCode, @ReportType, @Status, @ReportData::jsonb, @CreatedBy, @CreatedAt) RETURNING id";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(query, report);
    }
}