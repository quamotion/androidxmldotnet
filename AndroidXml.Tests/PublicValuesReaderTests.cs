using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidXml.Tests
{
    [TestClass]
    public class PublicValuesReaderTests
    {
        private static bool failed;

        [TestMethod]
        public void GetValuesConcurrencyTest()
        {
            // Make sure multiple threads can access the Values property in parallel.
            Collection<Thread> threads = new Collection<Thread>();
            for (int i = 0; i < 10; i++)
            {
                threads.Add(new Thread(GetValue));
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            while (threads.Any(t => t.IsAlive))
            {
                Thread.Sleep(100);
            }

            Assert.IsFalse(failed);
        }

        private static void GetValue()
        {
            try
            {
                var value = PublicValuesReader.Values;
            }
            catch(Exception)
            {
                failed = true;
            }
        }
    }
}
