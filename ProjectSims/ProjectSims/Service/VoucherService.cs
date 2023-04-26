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
    public class VoucherService
    {
        private VoucherRepository vouchers;

        public VoucherService()
        {
            vouchers = new VoucherRepository();
        }
        public int GetNextId()
        {
            return vouchers.GetNextId();
        }
        public List<Voucher> GetAllVouchers()
        {
            return vouchers.GetAll();
        }
        public Voucher GetVoucherById(int id)
        {
            return vouchers.GetById(id);
        }
        public List<Voucher> GetVouchersWithIds(List<int> ids)
        {
            return vouchers.GetWithIds(ids);
        }
        public List<Voucher> GetActiveVouchersWithIds(List<int> ids)
        {
            return vouchers.GetActiveVouchers(ids);
        }
        public void Create(Voucher voucher)
        {
            vouchers.Create(voucher);
        }
        public void Delete(Voucher voucher)
        {
            vouchers.Remove(voucher);
        }
        public void Update(Voucher voucher)
        {
            vouchers.Update(voucher);
        }
        public void Subscribe(IObserver observer)
        {
            vouchers.Subscribe(observer);
        }
    }
}
