// Copyright (c) 2015 Quamotion
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Xunit;

namespace AndroidXml.Tests
{
    public class PublicValuesReaderTests
    {
        private static bool failed;

        [Fact]
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

            Assert.False(failed);
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
