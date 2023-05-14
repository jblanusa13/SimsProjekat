using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class LastShown : ISerializable
    {
        public DateOnly Date { get; set; }
        public LastShown() { }
        public LastShown(DateOnly date)
        {
            Date = date;
        }
        public void FromCSV(string[] values)
        {
            Date = DateOnly.ParseExact(values[0], "dd.MM.yyyy");
        }

        public string[] ToCSV()
        {
            string[] csvvalues = {
                Date.ToString("dd.MM.yyyy")
            };
            return csvvalues;
        }
    }
}
