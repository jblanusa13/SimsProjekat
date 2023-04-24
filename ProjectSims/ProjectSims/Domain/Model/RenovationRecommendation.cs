using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class RenovationRecommendation : ISerializable
    {
        public int Id { get; set; }
        public int RenovationUrgency { get; set; }
        public string Recommendations { get; set; }

        public RenovationRecommendation() { }

        public RenovationRecommendation(int id, int renovationUrgency, string recommendations)
        {
            Id = id;
            RenovationUrgency = renovationUrgency;
            Recommendations = recommendations;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            RenovationUrgency = Convert.ToInt32(values[1]);
            Recommendations = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(), 
                RenovationUrgency.ToString(),
                Recommendations
            };
            return csvValues;
        }
    }
}
