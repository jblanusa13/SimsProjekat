using ProjectSims.FileHandler;
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
        public int GetNextId()
        {
            if (vouchers.Count == 0)
            {
                return 0;
            }
            return vouchers.Max(t => t.Id) + 1;
        }
        public void Create(Voucher voucher)
        {
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
