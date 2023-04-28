using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IGenericRepository<T, ID>
    {
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Remove(T entity);
        T GetById(ID key);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
