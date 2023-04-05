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
        public List<Voucher> GetAllVouchers()
        {
            return vouchers.GetAll();
        }
        public void Create(Voucher voucher)
        {
             vouchers.Add(voucher);
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
