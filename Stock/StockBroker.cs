using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace StockLibrary
{
    public class StockBroker // SUBSCRIBER
    {
        public string BrokerName { get; set; }

        public List<Stock> stocks = new List<Stock>();

        // Represents a lock that is used to manage access to a resource, allows one thread to be
        // in write mode with exclusive ownership of the lock.
        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"C:\Users\<some path>\output.txt";
        public string titles = "Broker".PadRight(10) 
                             + "Stock".PadRight(15)
                             + "Value".PadRight(10) 
                             + "Changes".PadRight(10) 
                             + "Date and Time";
        
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }
        
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            
            // registers StockBroker as a subscriber to the event, "StockValueChanged"
            // to be notified whenever the value of the stock being added is changed.
            // set this Stock's EventHandler delegate to this StockBroker's EventHandler
            stock.StockValueChanged += stock_StockValueChanged;
            // publisher.EventInterestedIn += subscriber's EventHandler
        }

        // Event Handler
        void stock_StockValueChanged(Object sender, StockNotification sn) // REVIEW (The "Notify" Method?), (EventArgs e -> StockNotification sn?)
        {
            myLock.EnterWriteLock(); // The EventerWriteLock method is used to enter the lock in write mode.
            // When a thread is in write mode, no other thread can enter the lock in any mode.
            try
            {
                Stock newStock = (Stock) sender;
                string statement = BrokerName.PadRight(10);
                statement += sn.StockName.PadRight(15);
                statement += sn.CurrentValue.ToString().PadRight(10);
                statement += sn.NumChanges.ToString().PadRight(10);
                statement += DateTime.Now.ToString();

                // Create the file to write to with the titles if it does not exist yet.
                if (!File.Exists(docPath))
                {
                    using (StreamWriter sw = File.CreateText(docPath))
                    {
                        sw.WriteLine(titles);
                        Console.WriteLine(titles);
                    }
                }

                // Writes to a text file the statement message received from the Publisher.
                using (StreamWriter outputFile = new StreamWriter(docPath, true))
                {
                    outputFile.WriteLine(statement);
                    Console.WriteLine(statement);
                }
            }
            finally
            {
                myLock.ExitWriteLock();
            }
        }
    }
}
