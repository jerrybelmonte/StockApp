using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockLibrary
{
    public class Stock // PUBLISHER
    {
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
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500); // 1/2 second / (Every 500 milliseconds...)
                ChangeStockValue(); // (This stock's value is modified...)
            } // (& repeats 25 times after the stock object is created.)
        }
        
        public void ChangeStockValue() // StartProcess() / XXXXX (The Stock is being created & the changes to its value are made BEFORE the StockBroker can subscribe.)
        {
            // (create new StockNotification event, "data")
            var data = new StockNotification();

            var rand = new Random();
            CurrentValue += rand.Next(MaxChange); // the range within a stock can change every time unit
            NumChanges++;

            // (update data-members of the StockNotification, "data" accordingly)
            data.StockName = StockName;
            data.CurrentValue = CurrentValue;
            data.NumChanges = NumChanges;
            OnStockValueChanged(data); // ( pass the StockNotification w/ the updated data to OnStockValueChanged() )
        }
        
        protected virtual void OnStockValueChanged(StockNotification sn) {
            if ((CurrentValue - InitialValue) > Threshold) // REVIEW (what if it decreases??)
            {
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
            }
        }
    }
}
