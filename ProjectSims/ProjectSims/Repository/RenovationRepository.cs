using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Repository
{
    public class RenovationRepository : IRenovationRepository
    {
        private RenovationFileHandler renovationFileHandler;
        private List<Renovation> renovations;

        public RenovationRepository()
        {
            renovationFileHandler = new RenovationFileHandler();
            renovations = renovationFileHandler.Load();
        }

        public List<Renovation> GetAll()
        {
            return renovations;
        }

        public int NextId()
        {
            if (renovations.Count == 0)
            {
                return 0;
            }
            return renovations.Max(r => r.Id) + 1;
        }

        public void Create(Renovation entity)
        {
            renovations.Add(entity);
            renovationFileHandler.Save(renovations);
        }

        public void Update(Renovation entity)
        {
            int index = renovations.FindIndex(a => entity.Id == a.Id);
            if (index != -1)
            {
                renovations[index] = entity;
            }
            renovationFileHandler.Save(renovations);
        }

        public void Remove(Renovation entity)
        {
            renovations.Remove(entity);
            renovationFileHandler.Save(renovations);
        }

        public Renovation GetById(int key)
        {
            return renovations.Find(r => r.Id == key);
        }
    }
}
