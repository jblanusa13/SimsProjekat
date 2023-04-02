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
    public class VoucherController
    {
        private VoucherDAO vouchers;

        public VoucherController()
        {
            vouchers = new VoucherDAO();
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
