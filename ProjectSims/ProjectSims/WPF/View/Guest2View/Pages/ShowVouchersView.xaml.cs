using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for ShowVouchersView.xaml
    /// </summary>
    public partial class ShowVouchersView : Page
    {
        public Guest2 guest2 { get; set; }
        public ObservableCollection<Voucher> ListVoucher { get; set; }

        private VoucherRepository voucherRepository;
        public ShowVouchersView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            voucherRepository = new VoucherRepository();

            ListVoucher = new ObservableCollection<Voucher>(GetVouchersWithIds(guest2.VoucherIds));
        }

        private List<Voucher> GetVouchersWithIds(List<int> ids)
        {
            List<Voucher> vouchers = new List<Voucher>();
            foreach(int id in ids)
            {
                vouchers.Add(voucherRepository.GetVoucherById(id));
            }

            return vouchers;
        }
    }
}
