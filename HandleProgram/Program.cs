using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace HandleProgram
{
    class Program
    {
        private static Mutex mutex;
        const string fileName = @"D:\abc.txt";
        static void Main(string[] args)
        {
            
            mutex = new Mutex();
            
            for (int i = 0; i <= 5; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(Run));
                thread.Start(i);
            }

            FileStream fileStream = File.Open(fileName, FileMode.Create);
            OSHandle oSHandle = new OSHandle(fileStream.Handle);
            
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine("abc123");
            streamWriter.Flush();
            
            oSHandle.Dispose();
            try
            {
                streamWriter.WriteLine("qwe123");
                streamWriter.Flush();
                streamWriter.Close();
            } catch (Exception e)
            {
                Console.WriteLine("File is already closed");
            }

            Thread.Sleep(2000);
            
            Console.WriteLine("Main thread exits");
        }

        private static void Run(object num)
        {
            Console.WriteLine("Thread {0} is working", num);

            mutex.Lock();
            Console.WriteLine("Thread {0} locked params", num);

            Thread.Sleep(1000);

            Console.WriteLine("Thread {0} unlocked params", num);
            mutex.Unlock();
        }

    }
}
