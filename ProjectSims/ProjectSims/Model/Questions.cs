using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class Questions : ISerializable
    {
        public int Id { get; set; }
        public string Question { get; set; }

        public Questions() { }

        public Questions(int id, string question)
        {
            Question = question;
            Id = id;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Question = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Question };
            return csvValues;
        }
    }
}
