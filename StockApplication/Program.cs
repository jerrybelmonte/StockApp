using System;
using StockLibrary;

namespace StockApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock1 = new Stock("Technology", 160, 5, 15); // Activate(): (.5 sec * 25 times) = 12.5 seconds
            Stock stock2 = new Stock("Retail", 30, 2, 6); // 12.5 seconds
            Stock stock3 = new Stock("Banking", 90, 4, 10); // 12.5 seconds
            Stock stock4 = new Stock("Commodity", 500, 20, 50); // 12.5 seconds
            
            // After 50 seconds...

            StockBroker b1 = new StockBroker("Broker 1");
            b1.AddStock(stock1);
            b1.AddStock(stock2);

            StockBroker b2 = new StockBroker("Broker 2");
            b2.AddStock(stock1);
            b2.AddStock(stock3);
            b2.AddStock(stock4);

            StockBroker b3 = new StockBroker("Broker 3");
            b3.AddStock(stock1);
            b3.AddStock(stock3);

            StockBroker b4 = new StockBroker("Broker 4");
            b4.AddStock(stock1);
            b4.AddStock(stock2);
            b4.AddStock(stock3);
            b4.AddStock(stock4);
        }
    }
}