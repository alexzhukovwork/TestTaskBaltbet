using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class Service : IService1
    {
        public string GetData(int value)
        {
            string [] arr = {"pidor", "pidoriwe", "pidrinbo"};

            return arr[value];
        }
    }
}