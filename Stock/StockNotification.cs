using System;

namespace StockLibrary
{
    public class StockNotification : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }
        public DateTime ValueChangedTime { get; } = DateTime.Now;

        /// <summary>
        ///     Stock notification attributes that are set and changed
        /// </summary>
        /// <param name="stockName">Name of stock</param>
        /// <param name="currentValue">Current value of the stock</param>
        /// <param name="numChanges">Number of changes the stock goes through</param>
        public StockNotification(string stockName, int currentValue, int numChanges)
        {
            StockName = stockName;
            CurrentValue = currentValue;
            NumChanges = numChanges;
        }

        public override string ToString()
        {
            return $"{StockName,-15}{CurrentValue,-10}{NumChanges,-10}{ValueChangedTime}";
        }
    }
}