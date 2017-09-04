using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBTTest.Data.Domain;
using UBTTest.Business.Models;

namespace UBTTest.Business
{
    public interface IDataService
    {
        void AddModelsBySelectedYear(Model model);
        Make SearchModelsByMakeId(int id);
        List<Make> SearchMakeByFuzzyName(string input);
    }
}
