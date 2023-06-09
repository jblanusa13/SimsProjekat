using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    class Guest1Repository : IGuest1Repository
    {
        private Guest1FileHandler guestFileHandler;
        private List<Guest1> guests;

        public Guest1Repository()
        {
            guestFileHandler = new Guest1FileHandler();
            guests = guestFileHandler.Load();
        }

        public Guest1 GetByUserId(int userId)
        {
            return guests.FirstOrDefault(g => g.UserId == userId);
        }

        public List<Guest1> GetAll()
        {
            return guests;
        }

        public int NextId()
        {
            return guests.Max(t => t.Id) + 1;
        }

        public void Create(Guest1 entity)
        {
            entity.Id = NextId();
            guests.Add(entity);
            guestFileHandler.Save(guests);
        }
        public void Update(Guest1 entity)
        {
            int index = guests.FindIndex(guest => guest.Id == entity.Id);
            if (index != -1)
            {
                guests[index] = entity;
            }
            guestFileHandler.Save(guests);
        }

        public void Remove(Guest1 entity)
        { 
            guests.Remove(entity);
            guestFileHandler.Save(guests);
        }
        public Guest1 GetById(int key)
        {
            return guests.Find(guest => guest.Id == key);
        }
    }
}