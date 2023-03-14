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
        public string Adress { get; set; }
        public string Email { get; set; }
        public List<Accommodation> OwnersAccommodations { get; set; }

        public Owner() 
        {
            OwnersAccommodations = new List<Accommodation>();
        }

        public Owner(int id, string ownerName, string surname, string adress, string email)
        {
            Id = id;
            OwnerName = ownerName;
            Surname = Surname;
            Adress = adress;
            Email=email;
            OwnersAccommodations = new List<Accommodation>();
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerName = values[1];
            Surname = values[2];
            Adress = values[3];
            Email = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), OwnerName, Surname, Adress, Email };
            return csvvalues;
        }
    }
}
