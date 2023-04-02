using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }

        public DateTime ExpirationDate { get; set; }
        public Voucher()
        {

        }
        public Voucher(int id, DateTime expirationDate )
        {
            Id = id;
            ExpirationDate = expirationDate;
           
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ExpirationDate = DateTime.ParseExact(values[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), ExpirationDate.ToString("MM/dd/yyyy")};
            return csvvalues;
        }
    }
}
