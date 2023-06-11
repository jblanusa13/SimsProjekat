using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
namespace ProjectSims.FileHandler
{
    public class AppointmentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/appointment.csv";


        private Serializer<Appointment> _serializer;
        public AppointmentFileHandler()
        {
            _serializer = new Serializer<Appointment>();
        }
        public List<Appointment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Appointment> appointment)
        {
            _serializer.ToCSV(FilePath, appointment);
        }
    }
}
