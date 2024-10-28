public static class ClientMapperExtension
{
    public static ClientReadDTO ClientToClientRead(this Client client)
    {
        return new ClientReadDTO()
        {
            Id = client.Id,
            FullName = client.FullName,
            UserName = client.UserName,
            Email = client.Email
        };
    }

    public static Client ClientUpdate(this Client client, ClientUpdateDTO clientUpdateDTO)
    {
        client.FullName = clientUpdateDTO.FullName;
        client.UserName = clientUpdateDTO.UserName;
        client.Email = clientUpdateDTO.Email;
        return client;
    }

    public static Client ClientCreateToClient(this ClientCreateDTO clientCreateDTO)
    {
        return new Client()
        {
            FullName = clientCreateDTO.FullName,
            UserName = clientCreateDTO.UserName,
            Email = clientCreateDTO.Email,
            Password = clientCreateDTO.Password 
        };
    }

    public static Client DeleteClient(this Client client)
    {
        client.IsDeleted = true;
        client.DeletedAt = DateTime.UtcNow;
        client.UpdatedAt = DateTime.UtcNow;
        client.Version += 1;
        return client;
    }
}
