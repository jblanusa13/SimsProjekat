using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IVoucherRepository : IGenericRepository<Voucher,int>, ISubject
    {
        public List<Voucher> GetWithIds(List<int> ids);
        public List<Voucher> GetActiveVouchers(List<int> ids);
    }
}
