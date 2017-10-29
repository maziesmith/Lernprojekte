using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;

namespace ocr
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //..
            var Ocr = new AutoOcr();
            var Result = Ocr.ReadPdf(@"C:\temp\2.pdf");
            Console.WriteLine(Result.Text);
            Console.ReadKey();
        }
    }
}
