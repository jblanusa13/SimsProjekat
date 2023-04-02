using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class Guest2 : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public int Vouchers { get; set; }

        public Guest2()
        {

        }
        public Guest2(int id, string name, string surname, string adress, string email, int userId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Adress = adress;
            Email = email;
            UserId = userId;
            Vouchers = 0;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Adress = values[3];
            Email = values[4];
            UserId = Convert.ToInt32(values[5]);
            Vouchers = Convert.ToInt32(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Name, Surname, Adress, Email, UserId.ToString(),Vouchers.ToString()};
            return csvvalues;
        }
    }
}
