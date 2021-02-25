using System;
using System.Threading;

namespace StockLibrary
{
    public class Stock
    {
        public event EventHandler<StockNotification> StockValueChanged;

        private readonly Thread _thread;

        public string StockName { get; set; } 
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }
        
        /// <summary>
        ///     Stock class that contains all the information and changes of the stock
        /// </summary>
        /// <param name="name">Stock name</param>
        /// <param name="startingValue">Starting stock value</param>
        /// <param name="maxChange">The max value change of the stock</param>
        /// <param name="threshold">The range for the stock</param>
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            StockName = name;
            InitialValue = startingValue;
            CurrentValue = InitialValue;
            MaxChange = maxChange;
            Threshold = threshold;
            NumChanges = 0;

            _thread = new Thread(() => Activate());
            _thread.Start();
        }
        
        /// <summary>
        ///     Activates the threads synchronizations
        /// </summary>
        public void Activate()
        {
            int halfSecond = 500;

            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(halfSecond);
                ChangeStockValue();
            }
        }
        
        /// <summary>
        ///     Changes the stock value and also raising the event of stock value changes
        /// </summary>
        public void ChangeStockValue()
        {
            var rand = new Random();
            CurrentValue += rand.Next(MaxChange);
            NumChanges++;

            if ((CurrentValue - InitialValue) > Threshold)
            {
                OnStockValueChanged(new StockNotification(StockName, CurrentValue, NumChanges));
            }
        }
        
        protected virtual void OnStockValueChanged(StockNotification sn)
        {
            StockValueChanged?.Invoke(this, sn);
        }
    }
}
