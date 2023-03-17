using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class Owner : ISerializable
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<Accommodation> OwnersAccommodations { get; set; }

        public Owner() 
        {
            OwnersAccommodations = new List<Accommodation>();
        }

        public Owner(int id, string ownerName, string surname, string address, string email)
        {
            Id = id;
            OwnerName = ownerName;
            Surname = Surname;
            Address = address;
            Email=email;
            OwnersAccommodations = new List<Accommodation>();
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerName = values[1];
            Surname = values[2];
            Address = values[3];
            Email = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), OwnerName, Surname, Address, Email };
            return csvvalues;
        }
    }
}
