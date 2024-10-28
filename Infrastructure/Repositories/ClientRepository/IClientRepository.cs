public interface IClientReposiory
{
    PaginationResponse<IEnumerable<ClientReadDTO>> GetClients(ClientFilter clientFilter);
    ClientReadDTO? GetClientById(int id);
    bool DeleteClient(int id);
    bool CreateClient(ClientCreateDTO clientCreateDTO);
    bool UpdateClient(ClientUpdateDTO clientUpdateDTO);
}