using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class ForumViewModel : IObserver
    {
        private ForumService forumService;
        public ObservableCollection<Forum> Forums { get; set; }
        public ForumViewModel(Guest1 guest)
        {
            forumService = new ForumService();
            Forums = new ObservableCollection<Forum>(forumService.GetAllForums());
        }
        public void Update()
        {
            Forums.Clear();
            foreach (Forum forum in forumService.GetAllForums())
            {
                Forums.Add(forum);
            }
        }
    }
}
