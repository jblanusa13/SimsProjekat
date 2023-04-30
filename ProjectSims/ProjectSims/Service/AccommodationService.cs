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
    public class AccommodationService
    {
        private AccommodationRepository accommodationRepository;
        // private LocationService locationService;

        public AccommodationService()
        {
            accommodationRepository = new AccommodationRepository();
            // locationService = new LocationService();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return accommodationRepository.GetAll();
        }/*
        public List<Accommodation> GetAllAccommodationsForGuestView()
        {
            List<Accommodation> accommodations = GetAllAccommodations();
            accommodations.ForEach(accommodation => accommodation.Location = locationService.GetLocation(accommodation.IdLocation));
            return accommodations;
        }*/
        public List<Accommodation> GetAllAccommodationsForGuestView()
        {
            return GetAllAccommodations();
        }

        public Accommodation GetAccommodation(int id)
        {
            return accommodationRepository.GetById(id);
        }

        public void Create(Accommodation accommodation)
        {
            accommodationRepository.Create(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            accommodationRepository.Remove(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            accommodationRepository.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            accommodationRepository.Subscribe(observer);
        }
    }
}
