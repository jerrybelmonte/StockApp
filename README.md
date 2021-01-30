# CECS 475 - Lab Assignment 3
Software Development with Frameworks

## Objective
Create an application that models the ups and downs of a particular stock.

- The value of a stock is assumed to change by plus or minus a specified number of units above or below its initial value.
- A collection of brokers who control the stock must receive this notification.
- The range within which the stock can change every time unit and the threshold above or below which collection of brokers who control the stock must be notified are specified when the stock is created (using its constructor).

## Task
- Design and implement a C# application that satisfies the specification given above. This application involves delegates, events and threads. 
### C# Delegate
- You begin by defining a delegate class `StockNotification`. This is shown below.
```csharp
public delegate void StockNotification(string stockName, int currentValue, int numberChanges);
```
- This delegate is designed so that when an event of this type is fired, the stock's name, value, and the number of changes in value can be sent to the listener.
### C# Event
- You are required to use built-in .NET `EventHandler` delegate in your solution.
### Stock class
- Create the class `Stock` with the following attributes:
  - Stock name
  - Stock initial value
  - Maximum change (the range within a stock can change every time unit)
  - Notification threshold (the threshold above or below which the collection of brokers who control the stock must be notified)
- You are required to implement other members in the class `Stock` that are needed.
- When a stock object is created, a thread is started. This thread causes the stock's value to be modified every 500 milliseconds.
- If its value changes from its initial value by more than the specified notification threshold, an event method is invoked.
- This invokes the `stockEvent` (of event-type `StockNotification`) and multicasts a notification to all listeners who have registered with `stockEvent`.
### Write text to a file
- Create another event to notify saving the following information to a file when the stock's threshold is reached: 
  - date and time, 
  - stock name, 
  - initial value 
  - and current value.
### StockBroker class
- Create the class `StockBroker` which has fields broker name and stocks, a List of `Stock`. This latter field is not used in this application but could be used to obtain the stocks currently controlled by a given broker.
- The `addStock` method registers the `Notify` listener with the stock (in addition to adding it to the list of stocks held by the broker).
- This `Notify` method outputs to the console the name, value, and the number of changes of the stock whose value is out of the range given the stock's notification threshold.

## Resources
https://www.tutorialspoint.com/csharp/csharp_multithreading.htm

https://docs.microsoft.com/en-us/dotnet/standard/threading/overview-of-synchronization-primitives

https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file?redirectedfrom=MSDN