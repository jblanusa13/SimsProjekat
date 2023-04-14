using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class Guest2Service
    {
        private Guest2Repository guests;
        private VoucherService voucherService;

        public Guest2Service()
        {
            guests = new Guest2Repository();
            voucherService = new VoucherService();
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
        public Guest2 GetGuestById(int id)
        {
            return guests.GetGuestById(id);
        }
        public int GetAgeOnTour(Guest2 guest,Tour tour)
        {
            int age = tour.StartOfTheTour.Year - guest.BirthDate.Year;
            if (guest.BirthDate.DayOfYear > DateTime.Now.DayOfYear)
                age--;
            return age;
        }
        public void GiveVoucher(int id)
        {
            Guest2 guest = guests.GetGuestById(id);
            Voucher voucher = new Voucher(voucherService.GetNextId(),DateTime.Now,DateTime.Now.AddYears(1),false);
            voucherService.Create(voucher);
            guest.VoucherIds.Add(voucher.Id);
            Update(guest);
        }
    }
}

