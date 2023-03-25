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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public List<int> AccommodationIds { get; set; }

        public Owner() 
        {
            AccommodationIds = new List<int>();
        }
        public Owner(int id, string name, string surname, string address, string email, int userId, List<int> accommodationIds)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Address = address;
            Email = email;
            UserId = userId;
            AccommodationIds = accommodationIds;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Address = values[3];
            Email = values[4];
            UserId = Convert.ToInt32(values[5]);
            List<int> accommodationIds = new List<int>();
            foreach (string value in values[6].Split(",")) 
            {
                accommodationIds.Add(Convert.ToInt32(value)); 
            }
            AccommodationIds = accommodationIds;
        }

        public string[] ToCSV()
        {
            string AccommodationIdsString = "";
            foreach (int id in AccommodationIds)
            {
                if (id != AccommodationIds.Last())
                {
                    AccommodationIdsString += id.ToString() + ",";
                }
            }
            AccommodationIdsString += AccommodationIds.Last();
            string[] csvvalues = { 
                Id.ToString(), 
                Name,
                Surname,
                Address,
                Email,
                UserId.ToString(),
                AccommodationIdsString
            };
            return csvvalues;
        }
    }
}
