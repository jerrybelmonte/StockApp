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

        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"C:\Users\keira\RiderProjects\StockApp\output.text"; //@"C:\Users\gbelm\source\repos\StockApplication\Lab3_output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15)
                                                     + "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";
        
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
            try
            {
                Stock newStock = (Stock) sender;
                string statement;

                // (Output the stock's name, value, numChanges if it's value is out-of-range.)
                Console.WriteLine(sn.StockName + ": " +
                                  sn.CurrentValue + ": " +
                                  sn.NumChanges);
            }
            catch (Exception ex)
            {
                // TODO
                Console.WriteLine("StockValueChanged: FALSE");
            }
        }
    }
}
