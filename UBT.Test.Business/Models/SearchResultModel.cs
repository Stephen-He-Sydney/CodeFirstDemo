using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBTTest.Data.Domain;

namespace UBTTest.Business.Models
{
    public class SearchResultModel
    {
        public SearchResultModel()
        {
            Models = new List<Model>();
        }
        public Make Make { get; set; }
        public List<Model> Models { get; set; }
    }
}
