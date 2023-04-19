using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class ShowVouchersViewModel    {
        public Guest2 guest2 { get; set; }
        public ObservableCollection<Voucher> ListVoucher { get; set; }

        private VoucherService voucherService;

        public ShowVouchersViewModel(Guest2 g) 
        {
            guest2 = g;
            voucherService = new VoucherService();
            ListVoucher = new ObservableCollection<Voucher>(voucherService.GetVouchersWithIds(guest2.VoucherIds));
        }
    }
}
