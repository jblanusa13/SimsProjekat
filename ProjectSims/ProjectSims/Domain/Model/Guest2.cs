using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class Guest2 : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<int> VoucherIds { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }

        public Guest2()
        {
            VoucherIds = new List<int>();
        }
        public Guest2(int id, string name, string surname, string adress, string email, int userId, DateTime birthDate, string phoneNumber)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Adress = adress;
            Email = email;
            UserId = userId;
            VoucherIds = new List<int>();
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Adress = values[3];
            Email = values[4];
            UserId = Convert.ToInt32(values[5]);
            if (values[6] != "")
            {
                foreach (string voucher in values[6].Split(","))
                {
                    int voucherId = Convert.ToInt32(voucher);
                    VoucherIds.Add(voucherId);
                }
            }
            BirthDate = DateTime.ParseExact(values[7], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            PhoneNumber = values[8];
        }

        public string[] ToCSV()
        {
            string VoucherString = "";
            if(VoucherIds.Count > 0)
            {
                foreach (int voucherId in VoucherIds)
                {
                    if (voucherId != VoucherIds.Last())
                    {
                        VoucherString += voucherId.ToString() + ",";
                    }
                }
                VoucherString += VoucherIds.Last();
            }
            string[] csvvalues = { Id.ToString(), Name, Surname, Adress, Email, UserId.ToString(),VoucherString,BirthDate.ToString("MM/dd/yyyy"), PhoneNumber};
            return csvvalues;
        }
    }
}
