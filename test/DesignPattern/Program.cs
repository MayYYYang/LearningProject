using DesignPattern.Patterns.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using ConsoleApplication1;
using Newtonsoft.Json;

namespace DesignPattern
{
    public class VeevaVaultConfigs
    {
        public int TotalPageCount { get; set; }
        public int SearchCount { get; set; }

        public string ExcludeStatus { get; set; }
        public string IncludeDocumentType { get; set; }
        public string SuccessStatus { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string ExcludeStatus = "Retired,Expired";
            string IncludeDocumentType = "ppt,pptx,doc,docx,mp3,mp4,pdf,jpg,png,xlsx,xls";
            string SuccessStatus = "Document downloaded successfully";
            VeevaVaultConfigs test = new VeevaVaultConfigs
            {
                TotalPageCount = 50,
                SearchCount = 100,
                ExcludeStatus = ExcludeStatus,
                IncludeDocumentType = IncludeDocumentType,
                SuccessStatus = SuccessStatus
            };
            var sds = JsonConvert.SerializeObject(test);
            
        }
            //List<int> m = new List<int>();
            //for(int i=0;i<100;i++)
            //{
            //    m.Add(i);


            //}
            //var test =m.Count() / 50;

            //var ddd = m.Skip(50).Take(50).ToList();

       
    
    
    }
    
}
