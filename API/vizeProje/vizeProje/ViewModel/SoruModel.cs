using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vizeProje.Models;

namespace vizeProje.ViewModel
{
    public class SoruModel
    {
        public int soruId { get; set; }


        public Nullable<int> kategori { get; set; }
        public Nullable<int> yazar { get; set; }

        public string soru1 { get; set; }


        

    }
}