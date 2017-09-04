using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBTTest.Data.Domain
{
    public class Make
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NiceName { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
