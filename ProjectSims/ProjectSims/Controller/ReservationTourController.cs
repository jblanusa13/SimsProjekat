﻿using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class ReservationTourController
    {
        private ReservationTourDAO reservations;
        

        public ReservationTourController()
        {
            reservations = new ReservationTourDAO();
        }

        public List<ReservationTour> GetAllReservations()
        {
            return reservations.GetAll();
        }

        public void Create(ReservationTour reservation)
        {
            reservations.Add(reservation);
        }

        public void Delete(ReservationTour reservation)
        {
            reservations.Remove(reservation);
        }
        public List<int> FindWaitingAndInvitedGuestIdsByTourId(int tourId)
        {
            List<int> guestIds = new List<int>();
            foreach (ReservationTour reservation in GetAllReservations())
            {
                if (reservation.TourId == tourId && (reservation.State == Guest2State.Waiting || reservation.State == Guest2State.Invited))
                {
                    guestIds.Add(reservation.Guest2Id);
                }
            }
            return guestIds;
        }
        public List<int> FindTourGuestIds(int tourId)
        {
            List<int> guestIds = new List<int>();
            foreach (ReservationTour reservation in GetAllReservations())
            {
                if (reservation.TourId == tourId)
                {
                    guestIds.Add(reservation.Guest2Id);
                }
            }
            return guestIds;
        }
        public List<int> FindPresentGuestIdsByTourId(int tourId)
        {
            List<int> guestIds = new List<int>();
            foreach (ReservationTour reservation in GetAllReservations())
            {
                if (reservation.TourId == tourId && reservation.State == Guest2State.Present)
                {
                    guestIds.Add(reservation.Guest2Id);
                }
            }
            return guestIds;
        }
        public void InviteGuests(int tourId)
        {
            List<ReservationTour> rezervacije = new List<ReservationTour>();
            
            foreach(ReservationTour reservation in GetAllReservations())
            {
                if(reservation.TourId == tourId && (int)reservation.State <= 1 )
                {
                    reservation.State = Guest2State.Invited;
                }
                rezervacije.Add(reservation);                  
            }

            foreach(ReservationTour reservation in rezervacije)
            {
                reservations.Update(reservation);
            }
            
        }
        public void NotifyGuest(int guestId,int tourId)
        {
            ReservationTour reservation = GetAllReservations().Find(r=> (r.Guest2Id == guestId && r.TourId==tourId));
            reservation.State=Guest2State.Waiting;
            reservations.Update(reservation);
        }

        public bool IsWaiting(int guest2Id)
        {
            foreach(ReservationTour reservation in GetAllReservations())
            {
                if (reservation != null && reservation.State == Guest2State.Waiting)
                {
                    return true;
                }
            }
            return false;
        }
        public void ConfirmPresence(int guest2Id,Tour tour)
        {
            ReservationTour reservation = GetAllReservations().Find(r => r.Guest2Id == guest2Id && r.TourId == tour.Id);
            reservation.State = Guest2State.Present;
            reservation.KeyPointWhereGuestArrivedId = tour.ActiveKeyPointId;
            reservations.Update(reservation);
        }
        public void FinishTour(int tourId)
        {
            List<ReservationTour> rezervacije = new List<ReservationTour>();
            foreach(ReservationTour reservation in reservations.GetAll())
            {
                if(reservation.TourId == tourId && reservation.State != Guest2State.Present)
                {
                    reservation.State = Guest2State.NotPresent;
                    reservation.KeyPointWhereGuestArrivedId = -1;
                }
                rezervacije.Add(reservation);

            }
            foreach(ReservationTour reservation in rezervacije)
            {
                reservations.Update(reservation);
            }
        }
        public void Update(ReservationTour reserevation)
        {
            reservations.Update(reserevation);
        }

        public void Subscribe(IObserver observer)
        {
            reservations.Subscribe(observer);
        }

    }
}
