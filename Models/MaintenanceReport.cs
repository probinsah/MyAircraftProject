public class MaintenanceReport
{
    public int Id { get; set; }
    public string AircraftCode { get; set; }
    public string ReportType { get; set; }
    public string Status { get; set; }
    public string ReportData { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}