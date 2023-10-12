using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScanning.Models
{
    public class Vertice
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class BoundingPoly
    {
        public List<Vertice> vertices { get; set; }
    }

    public class Receipt
    {
        public string locale { get; set; }
        public string description { get; set; }
        public BoundingPoly boundingPoly { get; set; }
    }

    public class BillLine
    {
        public int Number { get; set; }
        public string Text { get; set; }
        public int yValue { get; set; }
    }
}
