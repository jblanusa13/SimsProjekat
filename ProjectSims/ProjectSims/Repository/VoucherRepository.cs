﻿using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    class VoucherRepository : ISubject
    {

        private VoucherFileHandler voucherFile;
        private List<Voucher> vouchers;
        private List<IObserver> observers;

        public VoucherRepository()
        {
            voucherFile = new VoucherFileHandler();
            vouchers = voucherFile.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (vouchers.Count == 0)
            {
                return 0;
            }
            return vouchers.Max(t => t.Id) + 1;
        }
        public void Add(Voucher voucher)
        {
            voucher.Id = NextId();
            vouchers.Add(voucher);
            voucherFile.Save(vouchers);
            NotifyObservers();
        }
        public void Remove(Voucher voucher)
        {
            vouchers.Remove(voucher);
            voucherFile.Save(vouchers);
            NotifyObservers();
        }
        public void Update(Voucher voucher)
        {
            int index = vouchers.FindIndex(v => voucher.Id == v.Id);
            if (index != -1)
            {
                vouchers[index] = voucher;
            }
            voucherFile.Save(vouchers);
            NotifyObservers();
        }
        public List<Voucher> GetAll()
        {
            return vouchers;
        }

        public Voucher GetVoucherById(int id)
        {
            return vouchers.Find(v => v.Id == id);
        }
        public List<Voucher> GetVouchersWithIds(List<int> ids)
        {
            List<Voucher> vouchers = new List<Voucher>();
            foreach (int id in ids)
            {
                vouchers.Add(GetVoucherById(id));
            }

            return vouchers;
        }
        public List<Voucher> GetActiveVouchersWithIds(List<int> ids)
        {
            List<Voucher> vouchers = new List<Voucher>();
            foreach (int id in ids)
            {
                Voucher voucher = GetVoucherById(id);
                if (voucher.Used == false) {
                    vouchers.Add(voucher);
                }
            }

            return vouchers;
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
