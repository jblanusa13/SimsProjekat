using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class VoucherFileHandler
    {
        private const string FilePath = "../../../Resources/Data/voucher.csv";

        private Serializer<Voucher> _serializer;

        private List<Voucher> vouchers;

        public VoucherFileHandler()
        {
            _serializer = new Serializer<Voucher>();
            vouchers = _serializer.FromCSV(FilePath);
        }
        public List<Voucher> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(FilePath, vouchers);
        }
    }
}
