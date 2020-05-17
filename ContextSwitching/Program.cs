using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContextSwitching
{
    class Program
    {
        private static bool iscompleted;

        private static List<int> list = new List<int>();
        private static object mylock = new object();
        private static object mylock2 = new object();
        static void Main(string[] args)
        {
            //Task task = new Task(Demo);
            //task.Start();

            //Task<string> task2 = new Task<string>(Demo2);
            //task2.Start();
            //task2.Wait();

            //Console.WriteLine(task2.Result);

            var th1 = new Thread(new ThreadStart(AddtoList));
            var th2 = new Thread(new ThreadStart(AddtoList));
            var th3 = new Thread(new ThreadStart(AddtoList));
            th3.Start(); 
            th1.Start();
            th2.Start();
            
            th1.Join();
            th2.Join();
            th3.Join();
            foreach (int i in list)
            {
                Console.Write(i + " ");
            }
            Console.ReadKey();
            
        }
        private static Random random = new Random();
        public static void AddtoList()
        {
            for (int i = 0; i < 100; i++)
            {
               
                Thread.Sleep(random.Next(1, 500));
                lock (mylock)
                {
                    list.Add(i);
                }
            }
        }

        private static string Demo2()
        {
            Thread.Sleep(2000);
            return "Hello";
        }

        public static void Demo()
        {
            Console.WriteLine("Hello World");
        }
        private static void WorkerFunction()
        {
            Thread.Sleep(5000);
            Console.WriteLine("ABC");
        }
        
    }
}
