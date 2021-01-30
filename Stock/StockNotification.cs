using System;
using System.Collections.Generic;
using System.Text;

namespace StockLibrary
{
    public class StockNotification : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }

        /// <summary>
        ///     Stock notification attributes that are set and changed
        /// </summary>
        /// <param name="stockName">Name of stock</param>
        /// <param name="currentValue">Current value of the stock</param>
        /// <param name="numberChanges">Number of changes the stock goes through</param>
        public StockNotification(string stockName, int currentValue, int numberChanges)
        {
            StockName = stockName;
            CurrentValue = currentValue;
            NumChanges = numberChanges;
        }
    }
}