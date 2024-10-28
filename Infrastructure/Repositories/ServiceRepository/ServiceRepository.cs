public class ServiceRepository(QuickBookingDbContext context) : IServiceRepository
{
    public bool CreateService(ServiceCreateDTO serviceCreateDTO)
    {
        try
        {
            bool isExisted = context.Services.Any(x => x.Name.ToLower() == serviceCreateDTO.Name.ToLower() && x.IsDeleted == false);
            if (isExisted) return false;

            context.Services.Add(serviceCreateDTO.ServiceCreateToService());
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteService(int id)
    {
        try
        {
            Service? service = context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return false;
            service.DeleteService(); 
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public ServiceReadDTO? GetServiceById(int id)
    {
        try
        {
            Service? service = context.Services.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            return service?.ServiceToServiceRead();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public PaginationResponse<IEnumerable<ServiceReadDTO>> GetServices(ServiceFilter serviceFilter)
    {
        try
        {
            IQueryable<ServiceReadDTO> services = context.Services.Where(x => x.IsDeleted == false).Select(x => x.ServiceToServiceRead());

            services = services.Skip((serviceFilter.PageNumber - 1) * serviceFilter.PageSize).Take(serviceFilter.PageSize);

            List<ServiceReadDTO> _services = services.ToList();
            if (serviceFilter.Name != null)
                _services = _services.Where(x => x.Name.ToLower().Contains(serviceFilter.Name.ToLower())).ToList();
            if (serviceFilter.WorkerId != null)
                _services = _services.Where(x => x.WorkerId == serviceFilter.WorkerId).ToList();
            int totalRecords = context.Services.Where(x => x.IsDeleted == false).Count();

            return PaginationResponse<IEnumerable<ServiceReadDTO>>.Create(serviceFilter.PageNumber, serviceFilter.PageSize, totalRecords, _services);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return PaginationResponse<IEnumerable<ServiceReadDTO>>.Create(
                pageNumber: serviceFilter.PageNumber,
                pageSize: serviceFilter.PageSize,
                totalRecords: 0,
                data: Enumerable.Empty<ServiceReadDTO>()
            );
        }
    }

    public bool UpdateService(ServiceUpdateDTO serviceUpdateDTO)
    {
        try
        {
            Service? service = context.Services.FirstOrDefault(x => x.Id == serviceUpdateDTO.Id && x.IsDeleted == false);
            if (service == null) return false;

            service.ServiceUpdate(serviceUpdateDTO);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }
}
