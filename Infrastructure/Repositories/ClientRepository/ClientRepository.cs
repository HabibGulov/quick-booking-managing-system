
public class ClientRepository(QuickBookingDbContext context) : IClientReposiory
{
    public bool CreateClient(ClientCreateDTO clientCreateDTO)
    {
        try
        {
            bool isExisted = context.Clients.Any(x => x.UserName.ToLower() == clientCreateDTO.UserName.ToLower() && x.IsDeleted == false);
            if (isExisted) return false;

            context.Clients.Add(clientCreateDTO.ClientCreateToClient());
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteClient(int id)
    {
        try
        {
            Client? client = context.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null) return false;
            client.DeleteClient();
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public ClientReadDTO? GetClientById(int id)
    {
        try
        {
            Client? client = context.Clients.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            return client?.ClientToClientRead();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return new ClientReadDTO();
        }
    }

    public PaginationResponse<IEnumerable<ClientReadDTO>> GetClients(ClientFilter clientFilter)
    {
        try
        {
            IQueryable<ClientReadDTO> clients = context.Clients.Where(x => x.IsDeleted == false).Select(x => x.ClientToClientRead());


            clients = clients.Skip((clientFilter.PageNumber - 1) * clientFilter.PageSize).Take(clientFilter.PageSize);

            int totalRecords = context.Clients.Where(x => x.IsDeleted == false).Count();

            List<ClientReadDTO> _clients = clients.ToList();
            if(clientFilter.Username!=null)
            _clients = _clients.Where(x=>x.UserName==clientFilter.Username).ToList();

            return PaginationResponse<IEnumerable<ClientReadDTO>>.Create(clientFilter.PageNumber, clientFilter.PageSize, totalRecords, _clients);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return PaginationResponse<IEnumerable<ClientReadDTO>>.Create(
                pageNumber: clientFilter.PageNumber,
                pageSize: clientFilter.PageSize,
                totalRecords: 0,
                data: Enumerable.Empty<ClientReadDTO>()
            );
        }
    }

    public bool UpdateClient(ClientUpdateDTO clientUpdateDTO)
    {
        try
        {
            Client? client = context.Clients.FirstOrDefault(x => x.Id == clientUpdateDTO.Id && x.IsDeleted == false);
            if (client == null) return false;

            client.ClientUpdate(clientUpdateDTO);
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