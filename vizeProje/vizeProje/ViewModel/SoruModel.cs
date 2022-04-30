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
        public string soru1 { get; set; }
        public int cevap { get; set; }
        public int kategori { get; set; }
        public int yazar { get; set; }
        

    }
}