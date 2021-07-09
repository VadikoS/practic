using System;
using Cache;
using System.Threading;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan        time = new TimeSpan(0, 1, 0, 0, 0);
            Cache<string>   cache = new Cache<string>(time, 10);
            
            cache.Save("123", "wrkqrlkqrjlkqjrlkqjrlkqjrlqkjr");
            cache.Save("111", "messagemessage");
            cache.Save("11231", "mai"); 
            cache.Save("24044", "wqr11231312312");
            cache.Save("1231", "qqqqqqqqqqqqqq");

            Console.Write(cache.Get("1231"));
            try
            {
                Console.Write(cache.Get("12313132"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
