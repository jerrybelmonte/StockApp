using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockLibrary
{
    public class Stock // PUBLISHER
    {
        // The .NET Framework defines a generic delegate called System.EventHandler<> that satisfies these rules:
        // public delegate void EventHandler<TEventArgs>(object source, TEventArgs e) where TEventArgs : EventArgs;
        // The "StockValueChanged" event is associated w/ the EventHandler delegate & is raised in a method, "OnStockValueChanged()"
        public event EventHandler<StockNotification> StockValueChanged; // (built-in Event-Handler Delegate ("EventHandler<StockNotification>") in place of creating a delegate explicitly)

        private readonly Thread _thread;

        public string StockName { get; set; } 
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }
        
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            StockName = name;
            InitialValue = startingValue;
            CurrentValue = InitialValue;
            MaxChange = maxChange;
            Threshold = threshold;
            NumChanges = 0;

            // (When a stock object is created, a thread is started.)
            _thread = new Thread(() => Activate());
            _thread.Start();
            // ----------------------------------------------------- (1/31: Added these 2 lines & finally got output??)
        }
        
        public void Activate()
        {
            int halfSecond = 500; // 1/2 second / (Every 500 milliseconds...)

            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(halfSecond);
                ChangeStockValue(); // (This stock's value is modified...)
            } // (& repeats 25 times after the stock object is created.)
        }
        
        public void ChangeStockValue() // StartProcess() / XXXXX (The Stock is being created & the changes to its value are made BEFORE the StockBroker can subscribe.)
        {
            var rand = new Random(); // initialize a random number generator that will modify the stock value
            CurrentValue += rand.Next(MaxChange); // the range within a stock can change every time unit
            NumChanges++; // there will be a total of 25 changes to the stock from the Activate() method

            // Only raise the event of a stock value change when the change is above the threshold value
            if ((CurrentValue - InitialValue) > Threshold) 
            {
                // Fire a StockNotification event with the name of the stock, current stock value, and the number of changes.
                OnStockValueChanged(new StockNotification(StockName, CurrentValue, NumChanges));
            }
        }
        
        // The pattern requires that you write a protected virtual method that fires the event.
        // The name must match the name of the event, prefixed with the word "On",
        // and then accept a single EventArgs argument.
        protected virtual void OnStockValueChanged(StockNotification sn) {
            // REVIEW (what if it decreases??) I was looking at the output and it seems current value always increases
            // that is why in the rand.Next() method, I opted for the single parameter function which is in the range 0 to MaxChange.
            /* "StockValueChanged?.Invoke(this, sn);" is the same as the following code... -------------------------
                 
            EventHandler handler = StockValueChanged;
            if (handler != null) // ("StockValueChanged?.": checks to make sure at least 1 listener is registered to that event, "StockValueChanged")
            {
            handler(this, sn); // ("Invoke(this, sn)": "raises" the event ("StockValueChanged") ) / (Which stock? -> "this" stock / What changed? -> "sn")
            }
                 
            */ // https://stackoverflow.com/questions/12217632/calling-an-event-handler-in-c-sharp ----------------
                
            // ( passes the source of the event & the event's data ("sn"'s data) to be processed by the EventHandler )
            // notifies all listeners who have registered w/ this StockNotification event, "StockValueChanged"
            StockValueChanged?.Invoke(this, sn);  // XXXXX (Doesn't execute because StockValueChanged == null / no one is registered to the event, "StockValueChanged" @ this point)
            // To work robustly in multithreading scenerios, you need to assign the delegate to a temporary variable before testing and invoking it:
            // var temp = StockValueChanged; if (temp != null) temp (this, sn);
            // We can achieve the same functionality without the temp variable with the null-conditional operator:
            // StockValueChanged?Invoke(this, sn);
            // Being both thread-safe and succinct, this is the best general way to invoke events.
        }
    }
}
