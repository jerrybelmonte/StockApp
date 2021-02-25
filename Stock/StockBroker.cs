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

        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        
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
            myLock.EnterWriteLock();

            if (!outputFileCreated)
            {
                using (var writer = File.CreateText(docPath))
                {
                    writer.WriteLine(titles);
                    Console.WriteLine(titles);
                }
                outputFileCreated = true;
            }

            myLock.ExitWriteLock();
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
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                }
                finally
                {
                    Console.WriteLine(statement);
                }
            };
        }

        /// <summary>
        ///     The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="sn">The encapsulated stock notification event</param>
        void stock_StockValueChanged(Object sender, StockNotification sn)
        {
            myLock.EnterWriteLock();

            try
            {
                string statement = BrokerName.PadRight(10) + sn.ToString();

                using (var writer = File.AppendText(docPath))
                {
                    writer.WriteLine(statement);
                    Console.WriteLine(statement);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                myLock.ExitWriteLock();
            }
        }
    }
}
