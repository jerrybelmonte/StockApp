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
        
        private static bool outputFileCreated = false;
        static readonly string docPath = Directory.GetCurrentDirectory() + @"\..\..\..\..\output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";

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
                using (var writer = File.CreateText(docPath))
                {
                    writer.WriteLine(titles);
                    Console.WriteLine(titles);
                }
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
                string statement = BrokerName.PadRight(10) + args.ToString();
                try
                {
                    using (StreamWriter writer = File.AppendText(docPath))
                    {
                        await writer.WriteLineAsync(statement);
                    }
                    Console.WriteLine(statement);
                }
                catch (Exception) { }
            };
        }
    }
}
