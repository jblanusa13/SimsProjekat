using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims
{
    public class Injector
    {
        private static Dictionary<Type, object> implementations = new Dictionary<Type, object>
        {
            { typeof(IGuest2Repository), new Guest2Repository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(IReservationTourRepository), new ReservationTourRepository() },
            { typeof(ITourRatingRepository), new TourRatingRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(IGuideRepository), new GuideRepository() },
            { typeof(IKeyPointRepository), new KeyPointRepository() },
            { typeof(IUserRepository), new UserRepository() },            
            //{ typeof(IAccommodationRepository), new AccommodationRepository() },
            // { typeof(IGuestAcommodationRepository), new GuestAccommodationRepository() },
           // { typeof(ILocationRepository), new LocationRepository() },
            //{ typeof(IOwnerRepository), new OwnerRepository() },
            { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
            { typeof(IGuest1Repository), new Guest1Repository() },
            { typeof(IAccommodationRatingRepository), new AccommodationRatingRepository() },
            { typeof(IRenovationRecommendationRepository), new RenovationRecommendationRepository() },
            { typeof(IRequestRepository), new RequestRepository() }
        };
       public static T CreateInstance<T>()
        {
            Type type = typeof(T);
            if(Injector.implementations == null)
            {
                MessageBox.Show("ovo je null");
            }
            else
            {
                if (Injector.implementations.ContainsKey(type))
                {
                    return (T)Injector.implementations[type];
                }
                throw new ArgumentException($"No implementation found for type {type}");
           }
            return default(T);
        }
    }
}