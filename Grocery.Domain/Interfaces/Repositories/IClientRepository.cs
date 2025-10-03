﻿using Grocery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        public Client? Get(string email);
        public Client? Get(int id);
        public List<Client> GetAll();
    }
}
