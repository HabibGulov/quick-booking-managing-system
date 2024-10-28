public interface IServiceRepository
{
    bool CreateService(ServiceCreateDTO serviceCreateDTO);
    bool DeleteService(int id);
    ServiceReadDTO? GetServiceById(int id);
    PaginationResponse<IEnumerable<ServiceReadDTO>> GetServices(ServiceFilter serviceFilter);
    bool UpdateService(ServiceUpdateDTO serviceUpdateDTO);
}
