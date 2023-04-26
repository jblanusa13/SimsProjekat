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
using System.Windows.Shapes;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;

namespace ProjectSims.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MyRequests.xaml
    /// </summary>
    public partial class MyRequests : Window, IObserver
    {
        public ObservableCollection<Request> Requests { get; set; }
        private Guest1 guest;
        private RequestService requestService;
        public MyRequests(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            this.guest = guest;

            requestService = new RequestService();
            requestService.Subscribe(this);

            Requests = new ObservableCollection<Request>(requestService.GetAllRequestByGuest(guest.Id));
        }

        public void Update()
        {
            Requests.Clear();
            foreach(var request in requestService.GetAllRequestByGuest(guest.Id))
            {
                Requests.Add(request);
            }
        }
    }
}
