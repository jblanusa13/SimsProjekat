using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class ProfileViewModel
    {
        public Owner Owner { get; set; }
        private OwnerService ownerService;
        private UserService userService;
        private AccommodationRatingService accommodationRatingService;

        public ProfileViewModel(Owner o)
        {
            Owner = o;
            ownerService = new OwnerService();
            userService = new UserService();
            accommodationRatingService = new AccommodationRatingService();
            Owner.SuperOwner = accommodationRatingService.IsSuperowner(Owner);
        }

        public void ChangeProfile(string name, string username, string password, string email, string address, bool? isSuperOwner)
        {
            Owner ownerForUpdate = new Owner(Owner.Id, name.Split(" ")[0], name.Split(" ")[1], address, email, Owner.UserId, Owner.AccommodationIds, Convert.ToBoolean(isSuperOwner));
            ownerService.Update(ownerForUpdate);
            User userForUpdate = new User(Owner.UserId, username, password);
            userService.Update(userForUpdate);
        }
    }
}
