using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class KeyPoint
    {
        public string Name { get; set; }
        public int TourId { get; set; }
        public KeyPoint()
        {

        }
        public KeyPoint(string name, int tourId)
        {
            Name = name;
            TourId = tourId;
        } 


    }
}
