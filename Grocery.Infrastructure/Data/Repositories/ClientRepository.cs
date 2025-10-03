
using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;
using Grocery.Domain.ValueObjects;

namespace Grocery.Infrastructure.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> clients;

        public ClientRepository()
        {
            clients = [
                new Client(1, "M.J. Curie", "user1@mail.com", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08="),
                new Client(2, "H.H. Hermans", "user2@mail.com", "dOk+X+wt+MA9uIniRGKDFg==.QLvy72hdG8nWj1FyL75KoKeu4DUgu5B/HAHqTD2UFLU="),
                new Client(3, "A.J. Kwak", "user3@mail.com", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")
            ];
            
            // Set user3 as admin
            clients[2].Role = Role.Admin;
        }

        public Client? Get(string email)
        {
            return clients.FirstOrDefault(c => c.EmailAddress.Equals(email));
        }

        public Client? Get(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public List<Client> GetAll()
        {
            return clients;
        }
    }
}
