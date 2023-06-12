using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class Guide : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
        public string Adress { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Guide()
        {

        }
        public Guide(int id, string name, string surname, string adress, string email, DateOnly birthDate,string phoneNumber,int userId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Adress = adress;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            UserId = userId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Adress = values[3];
            Email = values[4];
            BirthDate = DateOnly.ParseExact(values[5], "dd/MM/yyyy");
            PhoneNumber = values[6];
            UserId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Name, Surname, Adress, Email,BirthDate.ToString("dd/MM/yyyy"),PhoneNumber, UserId.ToString() };
            return csvvalues;
        }
    }
}
