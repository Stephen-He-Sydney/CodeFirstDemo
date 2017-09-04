using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UBTTest.Web.Models
{
    public class MakeViewModel
    {
        public MakeViewModel()
        {
            Models = new List<ModelViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public List<ModelViewModel> Models { get; set; }
    }
}