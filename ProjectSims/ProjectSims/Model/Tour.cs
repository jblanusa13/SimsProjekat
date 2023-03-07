using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    internal class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Descrption { get; set; }
        public string Language { get; set; }
        public int MaxNumberGuests { get; set; }
        public string KeyPoints { get; set; }
        public List<DateTime> StartOfTheTour { get; set; }
        public int Duration { get; set; }
        public string Images { get; set; }


        //Implements
        public void FromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}
