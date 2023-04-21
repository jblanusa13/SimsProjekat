using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class RenovationRecommendation
    {
        public int Id { get; set; }
        public int RenovationUrgency { get; set; }
        public string Recommendations { get; set; }

        public RenovationRecommendation()
        {

        }

        public RenovationRecommendation(int id, int renovationUrgency, string recommendations)
        {
            Id = id;
            RenovationUrgency = renovationUrgency;
            Recommendations = recommendations;
        }


    }
}
