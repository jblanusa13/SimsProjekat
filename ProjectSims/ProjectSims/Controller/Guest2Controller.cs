using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class Guest2Controller
    {
        private Guest2DAO guests;

        public Guest2Controller()
        {
            guests = new Guest2DAO();
        }

        public List<Guest2> GetAllGuests()
        {
            return guests.GetAll();
        }

        public void Create(Guest2 guest)
        {
            guests.Add(guest);
        }

        public void Delete(Guest2 guest)
        {
            guests.Remove(guest);
        }

        public void Update(Guest2 guest)
        {
            guests.Update(guest);
        }

        public void Subscribe(IObserver observer)
        {
            guests.Subscribe(observer);
        }

        public Guest2 FindGuest2ById(int id)
        {
            return guests.FindById(id);
        }

    }
}

