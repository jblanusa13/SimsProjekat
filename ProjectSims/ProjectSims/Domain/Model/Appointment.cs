using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class Appointment : ISerializable
    {
        public int Id { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }

        public Appointment() { }
        public Appointment(int id,TimeOnly start, TimeOnly end)
        {
            Id = id;
            Start = start;
            End = end;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Start = TimeOnly.ParseExact(values[1], "HH:mm");
            End = TimeOnly.ParseExact(values[2], "HH:mm");
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Start.ToString("HH:mm"), End.ToString("HH:mm") };
            return csvvalues;
        }
    }
}
