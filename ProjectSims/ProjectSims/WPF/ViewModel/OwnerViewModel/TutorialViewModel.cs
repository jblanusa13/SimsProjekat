using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class TutorialViewModel
    {
        public Owner Owner { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand PlayCommand { get; set; }
        public RelayCommand CommentCommand { get; set; }
        public TutorialViewModel(Owner owner, NavigationService navService)
        {
            Owner = owner;
            NavService = navService;
        }
    }
}
