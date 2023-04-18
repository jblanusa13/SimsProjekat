﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using ProjectSims.FileHandler;

namespace ProjectSims.Service
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository reservationRepository;
        private List<AccommodationReservation> reservations;

        public AccommodationReservationService()
        {
            reservationRepository = new AccommodationReservationRepository();
            reservations = reservationRepository.GetAll();
        }

        public AccommodationReservation GetReservation(int id)
        {
            return reservationRepository.Get(id);
        }

        public List<AccommodationReservation> GetReservationByGuest(int guestId)
        {
            return reservationRepository.GetByGuest(guestId);
        }
        public AccommodationReservation GetReservation(int guestId, int accommodationId, DateOnly checkInDate, DateOnly checkOutDate)
        {
            AccommodationReservation reservation = null;
            List<AccommodationReservation> reservations = reservationRepository.GetByGuest(guestId);
            foreach (var item in reservations)
            {
                if (item.CheckInDate==checkInDate && item.CheckOutDate==checkOutDate && item.AccommodationId==accommodationId) 
                {
                    reservation = item;                    
                }
            }
            return reservation;
        }

        public int NextId()
        {
            return reservations.Max(r => r.Id) + 1;
        }
        public void CreateReservation(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            int id = NextId();
            AccommodationReservation reservation = new AccommodationReservation(id, accommodationId, guestId, checkIn, checkOut, guestNumber);
            reservationRepository.Add(reservation);
        }

        public void RemoveReservation(AccommodationReservation reservation)
        {
            reservationRepository.Remove(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            reservationRepository.Subscribe(observer);
        }
    }
}
