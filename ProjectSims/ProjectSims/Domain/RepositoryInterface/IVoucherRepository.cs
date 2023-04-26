using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IVoucherRepository
    {
        public void Create(Voucher voucher);
        public void Update(Voucher voucher);
        public void Remove(Voucher voucher);
        public Voucher GetById(int voucherId);
        public List<Voucher> GetAll();
        public List<Voucher> GetWithIds(List<int> ids);
        public List<Voucher> GetActiveVouchers(List<int> ids);
        public int GetNextId();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();

    }
}
