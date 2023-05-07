using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.Domain.Model
{
    public enum KeyPointType { First, Intermediate, Last }
    public class KeyPoint : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Finished { get; set; }
        public KeyPointType Type { get; set; }
        public KeyPoint() { }
        public KeyPoint(int id, string name, KeyPointType type)
        {
           Id = id;
           Name = name;
           Finished = false;
           Type = type;

        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Finished = Convert.ToBoolean(values[2]);
            Type = (KeyPointType)Enum.Parse(typeof(KeyPointType),values[3]);
        }
        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Name, Finished.ToString(), Type.ToString() };
            return csvvalues;
        }


    }
}
