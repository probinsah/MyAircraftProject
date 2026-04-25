using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AircraftMaintenance, AircraftMaintenanceDto>()
            // Rename fields
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.AircraftCode))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.AircraftModel))
            .ForMember(dest => dest.Maintenance, opt => opt.MapFrom(src => src.MaintenanceType))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToUpper()))
            .ForMember(dest => dest.MaintenanceDate, opt => opt.MapFrom(src => src.MaintenanceDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.Engineer, opt => opt.MapFrom(src => src.EngineerName))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => $"{src.AircraftCode} - {src.MaintenanceType} ({src.Status})"));
    }
}