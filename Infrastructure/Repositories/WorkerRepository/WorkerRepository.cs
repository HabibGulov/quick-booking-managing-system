public class WorkerRepository(QuickBookingDbContext context) : IWorkerRepository
{
    public bool CreateWorker(WorkerCreateDTO workerCreateDTO)
    {
        try
        {
            bool isExisted = context.Workers.Any(x => x.UserName.ToLower() == workerCreateDTO.UserName.ToLower() && x.IsDeleted == false);
            if (isExisted) return false;

            context.Workers.Add(workerCreateDTO.WorkerCreateToWorker());
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteWorker(int id)
    {
        try
        {
            Worker? worker = context.Workers.FirstOrDefault(x => x.Id == id);
            if (worker == null) return false;
            worker.DeleteWorker();
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public WorkerReadDTO? GetWorkerById(int id)
    {
        try
        {
            Worker? worker = context.Workers.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            return worker?.WorkerToWorkerRead();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public PaginationResponse<IEnumerable<WorkerReadDTO>> GetWorkers(WorkerFilter workerFilter)
    {
        try
        {
            IQueryable<WorkerReadDTO> workers = context.Workers.Where(x => x.IsDeleted == false).Select(x => x.WorkerToWorkerRead());

            workers = workers.Skip((workerFilter.PageNumber - 1) * workerFilter.PageSize).Take(workerFilter.PageSize);

            int totalRecords = context.Workers.Where(x => x.IsDeleted == false).Count();

            List<WorkerReadDTO> _workers = workers.ToList();
            if (workerFilter.Profession != null)
                _workers = _workers.Where(x => x.Profession.ToLower().Contains(workerFilter.Profession.ToLower())).ToList();
            if (workerFilter.Username != null)
                _workers = _workers.Where(x => x.UserName.ToLower().Contains(workerFilter.Username.ToLower())).ToList();

            return PaginationResponse<IEnumerable<WorkerReadDTO>>.Create(workerFilter.PageNumber, workerFilter.PageSize, totalRecords, _workers);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return PaginationResponse<IEnumerable<WorkerReadDTO>>.Create(
                pageNumber: workerFilter.PageNumber,
                pageSize: workerFilter.PageSize,
                totalRecords: 0,
                data: Enumerable.Empty<WorkerReadDTO>()
            );
        }
    }

    public bool UpdateWorker(WorkerUpdateDTO workerUpdateDTO)
    {
        try
        {
            Worker? worker = context.Workers.FirstOrDefault(x => x.Id == workerUpdateDTO.Id && x.IsDeleted == false);
            if (worker == null) return false;

            worker.WorkerUpdate(workerUpdateDTO);
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
