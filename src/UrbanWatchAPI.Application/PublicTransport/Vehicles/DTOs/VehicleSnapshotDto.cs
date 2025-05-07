namespace UrbanWatchAPI.Application.PublicTransport.Vehicles.DTOs;

public class VehicleSnapshotDto
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public List<VehicleDto> Vehicles { get; set; } = new();
}