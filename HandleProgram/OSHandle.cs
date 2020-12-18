using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO;

namespace HandleProgram
{
    class OSHandle : IDisposable
    {
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CloseHandle(IntPtr handle);
        private IntPtr handle;

        public IntPtr Handle
        {
            get
            {
                return handle;
            }
        }

        public OSHandle(IntPtr handle)
        {
            this.handle = handle;
            Console.WriteLine("Handle {0} was created", handle.ToInt64());
        }

        public void Finalize()
        {
            if (handle != IntPtr.Zero)
            {
                bool isClosed = CloseHandle(handle);
                if (!isClosed)
                {
                    throw new Exception("This handle can't be closed'");
                }
                Console.WriteLine("Handle " + handle.ToInt64() + " was closed");
            }
        }
        public void Dispose()
        {
            Finalize();
        }
    }
}
