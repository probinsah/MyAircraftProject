public class AircraftMaintenance
{
    public int Id { get; set; }
    public string AircraftCode { get; set; }
    public string AircraftModel { get; set; }
    public string MaintenanceType { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string EngineerName { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}