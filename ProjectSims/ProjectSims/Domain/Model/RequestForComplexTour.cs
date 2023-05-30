using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class RequestForComplexTour : ISerializable
    {
        public int Id { get; set; }
        public int Guest2Id { get; set; }
        public Guest2 Guest2 { get; set; }
        public List<int> RequestIds { get; set; }
        public List<TourRequest> TourRequests { get; set; }
        public int NumberRequest { get; set;}
        public TourRequestState State { get; set; }

        public RequestForComplexTour()
        {
            RequestIds = new List<int>();
            TourRequests = new List<TourRequest>();
        }

        public RequestForComplexTour( int guest2Id, List<int> requestsIds, int numberRequest, TourRequestState state)
        { 
            Guest2Id = guest2Id;
            RequestIds = requestsIds;
            NumberRequest = numberRequest;
            State = state;
        }

        public string[] ToCSV()
        {
            string requestListIds = "";
            foreach (int requestId in RequestIds)
            {
                if (requestId != RequestIds.Last())
                {
                    requestListIds += requestId.ToString() + ",";
                }
            }
            requestListIds += RequestIds.Last().ToString();
            string[] csvvalues = { Id.ToString(), Guest2Id.ToString(), requestListIds, NumberRequest.ToString(), State.ToString() };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest2Id = Convert.ToInt32(values[1]);
            foreach (string requestId in values[2].Split(","))
            {
                int idRequest = Convert.ToInt32(requestId);
                RequestIds.Add(idRequest);
            }
            NumberRequest = Convert.ToInt32(values[3]);
            State = (TourRequestState)Enum.Parse(typeof(TourRequestState), values[4]);
        }
    }
}
