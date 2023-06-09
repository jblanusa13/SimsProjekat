using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Used { get; set; }
        public bool ValidVoucher { get; set; }
        public Voucher()
        {

        }
        public Voucher(int id, DateTime creationDate, DateTime expirationDate, bool used, bool validVoucher)
        {
            Id = id;
            CreationDate = creationDate;
            ExpirationDate = expirationDate;
            Used = used;
            ValidVoucher = validVoucher;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CreationDate = DateTime.ParseExact(values[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            ExpirationDate = DateTime.ParseExact(values[2], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Used = Convert.ToBoolean(values[3]);
            ValidVoucher = Convert.ToBoolean(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), CreationDate.ToString("MM/dd/yyyy"), ExpirationDate.ToString("MM/dd/yyyy"),
                Used.ToString(),ValidVoucher.ToString()};
            return csvvalues;
        }
    }
}
