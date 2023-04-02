﻿using ProjectSims.Model;
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
        private VoucherController voucherController;

        public Guest2Controller()
        {
            guests = new Guest2DAO();
            voucherController = new VoucherController();
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
        public void GiveVoucher(int id)
        {
            Guest2 guest = guests.FindById(id);
            Voucher voucher = new Voucher(-1, DateTime.Now.AddYears(1));
            voucherController.Create(voucher);
            guest.VoucherIds.Add(voucher.Id);
            Update(guest);
        }

    }
}

