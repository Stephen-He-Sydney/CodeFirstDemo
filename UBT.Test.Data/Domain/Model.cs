using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBTTest.Data.Domain
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NiceName { get; set; }

        public string Year { get; set; }

        public virtual Make Make { get; set; }

        public int MakeId { get; set; }
    }
}
