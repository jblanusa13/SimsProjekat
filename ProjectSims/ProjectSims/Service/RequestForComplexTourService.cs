using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public  class RequestForComplexTourService
    {
        private IRequestForComplexTourRepository complexTourRequestRepository;
        private IGuest2Repository guest2Repository;
        private ITourRequestRepository tourRequestRepository;
        public RequestForComplexTourService()
        {
            complexTourRequestRepository = Injector.CreateInstance<IRequestForComplexTourRepository>();
            guest2Repository = Injector.CreateInstance<IGuest2Repository>();
            tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            InitializeGuest();
            InitializeTourRequests();
        }
        private void InitializeGuest()
        {
            foreach (var item in complexTourRequestRepository.GetAll())
            {
                if(item.Guest2 != null)
                {
                    return;
                }
                item.Guest2 = guest2Repository.GetById(item.Guest2Id);
            }
        }
        private void InitializeTourRequests()
        {
            foreach (var item in complexTourRequestRepository.GetAll())
            {
                foreach(var requestId in item.RequestIds)
                {
                    if (!item.TourRequests.Contains(tourRequestRepository.GetById(requestId)))
                        item.TourRequests.Add(tourRequestRepository.GetById(requestId));
                }
            }
        }
        public int NextId()
        {
            return complexTourRequestRepository.NextId();
        }
        public List<RequestForComplexTour> GetAllRequests()
        {
            return complexTourRequestRepository.GetAll();
        }
        public List<RequestForComplexTour> GetByGuest2Id(int guest2Id)
        {
            return complexTourRequestRepository.GetByGuest2Id(guest2Id);
        }
        public List<int> GetGuidesWhoAcceptedSimpleRequestsIds(int id)
        {
            RequestForComplexTour requestForComplexTour  = GetById(id);
            return requestForComplexTour.TourRequests.Select(r => r.GuideId).ToList();
        }
        public List<Tuple<DateTime, DateTime>> GetAcepptedPartsSchedule(int id)
        {
            List<Tuple<DateTime, DateTime>> scheduledAppointments = new List<Tuple<DateTime, DateTime>>();
            RequestForComplexTour complexRequest = complexTourRequestRepository.GetById(id);
            foreach (TourRequest part in complexRequest.TourRequests)
            {
                if (part.State == TourRequestState.Accepted)
                {
                    scheduledAppointments.Add(new Tuple<DateTime, DateTime>(part.AcceptedStartOfAppointment, part.AcceptedStartOfAppointment));
                }
            }
            return scheduledAppointments.OrderBy(x => x.Item1).ToList();
        }
        public List<Tuple<DateTime, DateTime>> GetFreeAppointmentsByDate(TourRequest simpleRequest,DateTime date)
        {
            RequestForComplexTour complexRequest = complexTourRequestRepository.GetBySimpleRequestId(simpleRequest.Id);
            List<Tuple<DateTime, DateTime>> scheduledAppointmnets = GetAcepptedPartsSchedule(complexRequest.Id);    
            DateTime dayBegin = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime dayEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            if (scheduledAppointmnets.Count != 0)
            {
                List<Tuple<DateTime, DateTime>> freeAppointments = new List<Tuple<DateTime, DateTime>>();
                freeAppointments.Add(new Tuple<DateTime, DateTime>(dayBegin, scheduledAppointmnets.First().Item1));
                for (int i = 0; i < scheduledAppointmnets.Count - 1; i++)
                {
                    freeAppointments.Add(new Tuple<DateTime, DateTime>(scheduledAppointmnets[1].Item2, scheduledAppointmnets[i + 1].Item1));
                }
                freeAppointments.Add(new Tuple<DateTime, DateTime>(scheduledAppointmnets.Last().Item2, dayEnd));
                return freeAppointments;
            }
            return new List<Tuple<DateTime, DateTime>>() { new Tuple<DateTime, DateTime>(dayBegin, dayEnd) };
        }
        public bool CheckIfAppointmentIsAvailable(DateTime start, DateTime end,TourRequest simpleRequest)
        {
            foreach (var freeAppointment in GetFreeAppointmentsByDate(simpleRequest, start))
            {
                if ((start >= freeAppointment.Item1) && (start <= freeAppointment.Item2) && (end <= freeAppointment.Item2) && (end <= freeAppointment.Item2))
                    return true;
            }
            return false;
        }
        public RequestForComplexTour GetById(int id)
        {
            return complexTourRequestRepository.GetById(id);
        }
        public void Create(RequestForComplexTour tourRequest)
        {
            complexTourRequestRepository.Create(tourRequest);
        }
        public void Delete(RequestForComplexTour tourRequest)
        {
            complexTourRequestRepository.Remove(tourRequest);
        }
        public void Update(RequestForComplexTour tourRequest)
        {
            complexTourRequestRepository.Update(tourRequest);
        }
        public void Subscribe(IObserver observer)
        {
            complexTourRequestRepository.Subscribe(observer);
        }
    }
}
