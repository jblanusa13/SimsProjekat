﻿using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IOwnerRepository : IGenericRepository<Owner, int>, ISubject
    {
        public bool ExistAccommodation(Owner owner, int accommodationId);
        public void AddAccommodationId(Owner owner, int accommodationId);
    }
}
