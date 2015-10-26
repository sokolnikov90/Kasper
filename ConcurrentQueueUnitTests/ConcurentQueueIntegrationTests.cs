using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ConcurrentQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ConcurrentQueueUnitTests
{
    [TestFixture]
    public class ConcurentQueueIntegrationTests
    {
        [Test]
        public void Pop_ItemsFromEmptyQueue_CorrectCounters()
        {
            ConcurentQueue<int> concurentQueue = new ConcurentQueue<int>(5000);

            ConcurrentBag<Exception> popResult = new ConcurrentBag<Exception>();
            ConcurrentBag<int> pushItems = new ConcurrentBag<int>();
            Parallel.For(0, 10, i =>
            {
                pushItems.Add(i);
                try
                {
                    concurentQueue.Pop();
                }
                catch (Exception exp)
                {
                    popResult.Add(exp);
                }
            });

            Exception exception;
            popResult.TryPeek(out exception);
            Assert.IsInstanceOf(typeof(TimeoutException), exception);
            Assert.AreEqual(pushItems.Count, popResult.Count);
        }

        [Test]
        public void PopAndPush_CallPopBeforePush_CorrectCouners()
        {
            ConcurentQueue<int> concurentQueue = new ConcurentQueue<int>(10000);

            ConcurrentBag<int> popResult = new ConcurrentBag<int>();
            ConcurrentBag<Exception> popResultEx = new ConcurrentBag<Exception>();
            ConcurrentBag<int> pushItems = new ConcurrentBag<int>();
            Parallel.For(0, 10, i =>
            {
                pushItems.Add(i);
                try
                {
                    var temp = concurentQueue.Pop();
                    popResult.Add(temp);
                }
                catch (Exception exp)
                {
                    popResultEx.Add(exp);
                }
                concurentQueue.Push(i);
            });

            Exception exception;
            popResultEx.TryPeek(out exception);
            Assert.IsInstanceOf(typeof(TimeoutException), exception);
            Assert.AreEqual(pushItems.Count, popResult.Count + popResultEx.Count);
        }

        [Test]
        public void PopAndPush_CallPushBeforePop_CorrectCounters()
        {
            ConcurentQueue<int> concurentQueue = new ConcurentQueue<int>(10000);

            ConcurrentBag<int> popResult = new ConcurrentBag<int>();
            ConcurrentBag<Exception> popResultEx = new ConcurrentBag<Exception>();
            ConcurrentBag<int> pushItems = new ConcurrentBag<int>();
            Parallel.For(0, 10, i =>
            {
                concurentQueue.Push(i);
            });

            Parallel.For(0, 10, i =>
            {
                pushItems.Add(i);
                try
                {
                    var temp = concurentQueue.Pop();
                    popResult.Add(temp);
                }
                catch (Exception Exp)
                {
                    popResultEx.Add(Exp);
                }
            });

            Assert.AreEqual(pushItems.Count, popResult.Count + popResultEx.Count + concurentQueue.Count);
        }    
    }
}
