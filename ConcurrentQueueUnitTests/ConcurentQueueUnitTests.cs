using System;
using System.Collections.Generic;
using NUnit.Framework;
using ConcurrentQueue;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace ConcurrentQueueUnitTests
{
    [TestFixture]
    public class ConcurentQueueUnitTests
    {
        [Test]
        public void Push_10intBy10threads_CorrectCounters()
        {
            ConcurentQueue<int> concurentQueue = new ConcurentQueue<int>();

            Parallel.For(0, 10, i => concurentQueue.Push(i));

            Assert.AreEqual(10, concurentQueue.Count);
        }

        [Test]
        public void Pop_7intFromFilledQueue_CorrectCounters()
        {
            ConcurentQueue<int> concurentQueue = new ConcurentQueue<int>();
            Parallel.For(0, 10, i => concurentQueue.Push(i));

            ConcurrentBag<int> popResult = new ConcurrentBag<int>();
            Parallel.For(0, 7,  i => popResult.Add(concurentQueue.Pop()));

            Assert.AreEqual(3, concurentQueue.Count);
            Assert.AreEqual(7, popResult.Count);
        }

        [Test]
        public void Push_10strBy10threads_CorrectCounters()
        {
            ConcurentQueue<string> concurentQueue = new ConcurentQueue<string>();

            Parallel.For(0, 10, i => concurentQueue.Push(i.ToString()));

            Assert.AreEqual(10, concurentQueue.Count);
        }

        [Test]
        public void Pop_7strFromFilledQueue_CorrectCounters()
        {
            ConcurentQueue<String> concurentQueue = new ConcurentQueue<string>();
            Parallel.For(0, 10, i => concurentQueue.Push(i.ToString()));

            ConcurrentBag<string> popResult = new ConcurrentBag<string>();
            Parallel.For(0, 7, i => popResult.Add(concurentQueue.Pop()));

            Assert.AreEqual(3, concurentQueue.Count);
            Assert.AreEqual(7, popResult.Count);
        }    
    }
}
