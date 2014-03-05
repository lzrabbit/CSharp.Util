using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Util;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var desc = EnumDesc.GetDesc(APIExceptionType.非法EAN请求);
        }
    }
    public enum APIExceptionType
    {
        非法请求,
        未知异常,
        身份验证失败,
        未找到RateMapping,
        EAN请求失败,
        非法EAN请求,
    }
}
