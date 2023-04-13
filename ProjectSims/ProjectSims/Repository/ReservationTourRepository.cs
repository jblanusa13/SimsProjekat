﻿using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    class ReservationTourRepository : ISubject
    {

        private ReservationTourFileHandler reservationFile;
        private List<ReservationTour> reservations;

        private List<IObserver> observers;

        public ReservationTourRepository()
        {
            reservationFile = new ReservationTourFileHandler();
            reservations = reservationFile.Load();
            observers = new List<IObserver>();
        }

        public int NextId()
        {
            if(reservations.Count == 0)
            {
                return 0;
            }
            return reservations.Max(r => r.Id) + 1;
        }
        public void Create(ReservationTour reservation)
        {
            reservation.Id = NextId();
            reservations.Add(reservation);
            reservationFile.Save(reservations);
            NotifyObservers();
        }
        public void Remove(ReservationTour reservation)
        {
            reservations.Remove(reservation);
            reservationFile.Save(reservations);
            NotifyObservers();
        }
        public void Update(ReservationTour reservation)
        {
            int index = reservations.FindIndex(r => reservation.Id == r.Id);
            if (index != -1)
            {
                reservations[index] = reservation;
            }
            reservationFile.Save(reservations);
            NotifyObservers();
        }
        public List<ReservationTour> GetAll()
        {
            return reservations;
        }
        public List<ReservationTour> GetReservationsByTourId(Tour tour)
        {
            return reservations.Where(r=>r.TourId == tour.Id).ToList();
        }
        public List<int> GetGuestIdsByStateAndTourId(Tour tour, Guest2State state)
        {
            List<ReservationTour> wantedReservations = reservations.Where(r=> r.State == state && r.TourId == tour.Id).ToList();
            List<int> guestIds = new List<int>();
            wantedReservations.ForEach(r => guestIds.Add(r.Guest2Id));  
            return guestIds;
        }
        public ReservationTour GetTourIdWhereGuestIsWaiting(Guest2 guest)
        {
            ReservationTour reservation = reservations.Find(r=> r.Guest2Id == guest.Id && r.State == Guest2State.Waiting);
            return reservation;
        }
        public ReservationTour GetReservationByGuestAndTour(Tour tour, Guest2 guest2)
        {
            ReservationTour reservationTour = new ReservationTour();

            foreach (var reservation in GetAll())
            {
                if(reservation.Guest2Id == guest2.Id && reservation.TourId == tour.Id)
                {
                    reservationTour = reservation;
                }
            }
                return reservationTour;
        }
        public int GetNumberOfGuestsWhoUsedVoucher(Tour tour)
        {
            int numberOfGuestsWhoUsedVoucher = 0;
            reservations.ForEach(r => { if (r.TourId == tour.Id && r.UsedVoucher) numberOfGuestsWhoUsedVoucher++; });
            return numberOfGuestsWhoUsedVoucher;
        }
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
