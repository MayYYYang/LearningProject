using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.DI
{
    internal interface IServiceClass
    {
        string ServerInfo();
    }

    internal class ServiceClassA : IServiceClass
    {
        public string ServerInfo()
        {
            return "My Is ServerClassA";
        }
    }

    internal class ServiceClassB : IServiceClass
    {
        public string ServerInfo()
        {
            return "My Is ServerClassB";
        }
    }
    // 1:Setter注入
    internal class ClientClass
    {
        private IServiceClass _service;
        public void Set_Service(IServiceClass service)
        {
            this._service = service;
        }
        public void ShowInfo()
        {
            Console.WriteLine(_service.ServerInfo());
        }
    }
}
