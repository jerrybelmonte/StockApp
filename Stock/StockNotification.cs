using System;
using System.Collections.Generic;
using System.Text;

namespace StockLibrary
{
    // KKKKK Custom EventArgs Class:
    // a predefined Framework class with no members (other than the static Empty property).
    // EventArgs is a base class for conveying information for an event.
    // We are subclassing EventArgs to convey a stock price change event being fired.
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
        public StockNotification(string stockName, int currentValue, int numberChanges)
        {
            StockName = stockName;
            CurrentValue = currentValue;
            NumChanges = numberChanges;
        }
    }
}