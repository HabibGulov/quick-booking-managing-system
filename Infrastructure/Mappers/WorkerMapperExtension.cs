public static class WorkerMapperExtension
{
    public static WorkerReadDTO WorkerToWorkerRead(this Worker worker)
    {
        return new WorkerReadDTO()
        {
            Id=worker.Id,
            FullName=worker.FullName,
            UserName=worker.UserName,
            Email=worker.Email,
            Profession=worker.Profession,
            WorkHours=worker.WorkHours,
            WorkPlace=worker.WorkPlace
        };  
    }

    public static Worker WorkerUpdate(this Worker worker, WorkerUpdateDTO workerUpdateDTO)
    {
        worker.FullName=workerUpdateDTO.FullName;
        worker.UserName=workerUpdateDTO.UserName;
        worker.Email=workerUpdateDTO.Email;
        worker.Profession=workerUpdateDTO.Profession;
        worker.WorkHours=workerUpdateDTO.WorkHours;
        worker.WorkPlace=workerUpdateDTO.WorkPlace;
        return worker;
    }

    public static Worker WorkerCreateToWorker(this WorkerCreateDTO workerCreateDTO)
    {
        return new Worker()
        {
            FullName=workerCreateDTO.FullName,
            UserName=workerCreateDTO.UserName,
            Email=workerCreateDTO.Email,
            Profession=workerCreateDTO.Profession,
            WorkHours=workerCreateDTO.WorkHours,
            WorkPlace=workerCreateDTO.WorkPlace,
            Password=workerCreateDTO.Password
        };
    }
    
    public static Worker DeleteWorker(this Worker worker)
    {
        worker.IsDeleted=true;
        worker.DeletedAt=DateTime.UtcNow;
        worker.UpdatedAt=DateTime.UtcNow;
        worker.Version+=1;
        return worker;
    }
}