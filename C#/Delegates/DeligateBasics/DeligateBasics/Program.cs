using System;

namespace DelegateBasics
{
    // Advanges of using Delegates:
    // 1. Code reusability
    // 2. Code Design Flexibility
    class Program
    {
        delegate void LogDel(string text);
        static void Main(string[] args)
        {
            //LogDel logDel = LogTextToScreen // refering to static methods
            //LogDel logDel = new Log().LogTextToScreen; // refering to instance method
            //Console.WriteLine("Enter your name: ");
            //var name = Console.ReadLine();
            //logDel(name);

            // MultiCast Delegate
            Log log = new Log();
            LogDel LogTextToScreenDel, LogTextToFileDel;
            LogTextToScreenDel = log.LogTextToScreen;
            LogTextToFileDel = log.LogTextToFile;

            LogDel multiLogDel = LogTextToScreenDel + LogTextToFileDel;
            Console.WriteLine("Enter your name: ");
            var name = Console.ReadLine();
            //multiLogDel(name);

            // Passing the delegate as argument.
            LogText(multiLogDel, name);
        }

        static void LogText(LogDel logDel, string text)
        {
            logDel(text);
        }

        //static void LogTextToScreen(string text)
        //{
        //    Console.WriteLine($"{DateTime.Now}: {text}");
        //}

        //static void LogTextToFile(string text)
        //{
        //    using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
        //    {
        //        sw.WriteLine($"{DateTime.Now}: {text}");
        //    }
        //}
    }

    public class Log
    {
        public void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now}: {text}");
        }

        public void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {text}");
            }
        }

    }
}
