using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class Guest2Service
    {
      // private IGuest2Repository guest2Repository;
       private Guest2Repository guest2Repository;
       private VoucherService voucherService;

        public Guest2Service()
        {
            guest2Repository = new Guest2Repository();
           // guest2Repository = Injector.CreateInstance<IGuest2Repository>();
            voucherService = new VoucherService();
        }
        public List<Guest2> GetAllGuests()
        {
            return guest2Repository.GetAll();
        }
        public void Create(Guest2 guest)
        {
            guest2Repository.Create(guest);
        }
        public void Delete(Guest2 guest)
        {
            guest2Repository.Remove(guest);
        }
        public void Update(Guest2 guest)
        {
            guest2Repository.Update(guest);
        }
        public void Subscribe(IObserver observer)
        {
            guest2Repository.Subscribe(observer);
        }
        public Guest2 GetGuestById(int id)
        {
            return guest2Repository.GetGuestById(id);
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
            Guest2 guest = guest2Repository.GetGuestById(id);
            Voucher voucher = new Voucher(voucherService.GetNextId(),DateTime.Now,DateTime.Now.AddYears(1),false);
            voucherService.Create(voucher);
            guest.VoucherIds.Add(voucher.Id);
            Update(guest);
        }
    }
}

