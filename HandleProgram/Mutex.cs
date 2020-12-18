using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HandleProgram
{
    class Mutex
    {
        private volatile static int state = 0;
        private const int lockedState = 1;
        private const int unlockedState = 0;

        public void Lock() {
            while (Interlocked.CompareExchange(ref state, lockedState, unlockedState) != unlockedState)
            {
                Thread.Sleep(100);
            }
        }
        public void Unlock() {
            while (Interlocked.CompareExchange(ref state, unlockedState, lockedState) != lockedState)
            {
                Thread.Sleep(10);
            }
        }
    }
}
