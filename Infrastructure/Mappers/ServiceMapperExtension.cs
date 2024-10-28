public static class ServiceMapperExtension
{
    public static ServiceReadDTO ServiceToServiceRead(this Service service)
    {
        return new ServiceReadDTO()
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price,
            Duration = service.Duration,
            WorkerId = service.WorkerId
        };
    }

    public static Service ServiceUpdate(this Service service, ServiceUpdateDTO serviceUpdateDTO)
    {
        service.Name = serviceUpdateDTO.Name;
        service.Description = serviceUpdateDTO.Description;
        service.Price = serviceUpdateDTO.Price;
        service.Duration = serviceUpdateDTO.Duration;
        service.WorkerId = serviceUpdateDTO.WorkerId;
        return service;
    }

    public static Service ServiceCreateToService(this ServiceCreateDTO serviceCreateDTO)
    {
        return new Service()
        {
            Name = serviceCreateDTO.Name,
            Description = serviceCreateDTO.Description,
            Price = serviceCreateDTO.Price,
            Duration = serviceCreateDTO.Duration,
            WorkerId = serviceCreateDTO.WorkerId
        };
    }

    public static Service DeleteService(this Service service)
    {
        service.IsDeleted = true;
        service.DeletedAt = DateTime.UtcNow;
        service.UpdatedAt = DateTime.UtcNow;
        service.Version += 1;
        return service;
    }
}
