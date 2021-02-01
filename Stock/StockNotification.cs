using System;
using System.Collections.Generic;
using System.Text;

namespace StockLibrary
{
    // KKKKK Custom EventArgs Class:
    public class StockNotification : EventArgs
    {
        // KKKKK Allows for 1+ data values to be passed to the "On_____()"
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }


        /// <summary>
        ///     Stock notification attributes that are set and changed
        /// </summary>
        /// <param name="stockName">Name of stock</param>
        /// <param name="currentValue">Current value of the stock</param>
        /// <param name="numberChanges">Number of changes the stock goes through</param>
        
        public StockNotification()
        {
            StockName = "none";
            CurrentValue = 0;
            NumChanges = 0;
        }
        public StockNotification(string stockName, int currentValue, int numberChanges)
        {
            StockName = stockName;
            CurrentValue = currentValue;
            NumChanges = numberChanges;
        }
    }
}