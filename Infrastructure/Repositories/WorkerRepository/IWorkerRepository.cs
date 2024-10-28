public interface IWorkerRepository
{
    bool CreateWorker(WorkerCreateDTO workerCreateDTO);
    bool DeleteWorker(int id);
    WorkerReadDTO? GetWorkerById(int id);
    PaginationResponse<IEnumerable<WorkerReadDTO>> GetWorkers(WorkerFilter workerFilter);
    bool UpdateWorker(WorkerUpdateDTO workerUpdateDTO);
}
