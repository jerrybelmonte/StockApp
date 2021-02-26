using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StockLibrary
{
    public class StockBroker
    {
        public string BrokerName { get; set; }

        public List<Stock> stocks = new List<Stock>();

        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        
        private static bool outputFileCreated = false;
        static readonly string docPath = Directory.GetCurrentDirectory() + @"\..\..\..\..\output.txt";
        public string titles = $"{"Broker",-10}{"Stock",-15}{"Value",-10}{"Changes",-10}Date and Time";

        /// <summary>
        ///     The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName) 
        { 
            BrokerName = brokerName;
            CheckOutputFileCreated();
        }

        private void CheckOutputFileCreated()
        {
            if (!outputFileCreated)
            {
                File.WriteAllText(docPath, $"{titles}{Environment.NewLine}");
                Console.WriteLine(titles);
                outputFileCreated = true;
            }
        }

        /// <summary>
        ///     Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockValueChanged += async (sender, args) => 
            {
                string statement = $"{BrokerName,-10}{args}";
                await WriteStatementAsync(statement);
            };
        }

        async Task WriteStatementAsync(string text)
        {
            await _semaphore.WaitAsync();
            try
            {
                using (FileStream stream = new FileStream(docPath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    await writer.WriteLineAsync(text);
                    Console.WriteLine(text);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
