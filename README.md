# Stock Price Change Application
Software Development with Frameworks

## Table of Contents
- [Objective](#objective)
- [Introduction](#introduction)
- [Features](#features)
- [Technologies](#technologies)
- [How To Set Up](#how-to-set-up)
- [Authors](#authors)


## Objective
Create an application that models the ups and downs of a particular stock.


## Introduction
For this project we designed and implemented a C# application that satisfies the specifications given below: 

- The value of a stock is assumed to change by plus or minus a specified number of units above or below its initial value.
- A collection of brokers who control the stock must receive a notification when the stock price changes.
- The range within which the stock can change every time unit, and the threshold above or below which the collection of brokers who control the stock must be notified, are specified when the stock is created.

This application involves the use of C# delegates, events, multi-threading, and asynchronous programming. 


## Features

### C# Delegate
- We defined a delegate class `StockNotification`.
- This delegate is designed so that when an event of this type is fired, the stock's name, value, and the number of changes in value, can be sent to the listener.

### C# Event
- We used the built-in .NET `EventHandler` delegate in our solution.

### Stock class
- The class `Stock` has the following attributes:
  - Stock name
  - Stock initial value
  - Maximum change (the range a stock can change for every time unit)
  - Notification threshold (the threshold above or below which the collection of brokers who control the stock must be notified)

- When a stock object is created, a thread is started. This thread causes the stock's value to be modified every 500 milliseconds.
- If its value changes from its initial value by more than the specified notification threshold, an event method is invoked.
- This invokes the `stockEvent` (of event-type `StockNotification`) and multicasts a notification to all listeners who have registered with `stockEvent`.

### StockBroker class
- The class `StockBroker` has fields broker name and stocks, a List of `Stock`.
- The `addStock` method registers the `Notify` listener with the stock (in addition to adding it to the list of stocks held by the broker).
- The `Notify` method outputs to the console: the name, value, and the number of changes of the stock, whose value is out of the range given to the stock's notification threshold.

### Write to a text file
- We created another event to notify saving the following information to a file when the stock's threshold is reached: 
  - date and time
  - stock name
  - initial value 
  - current value.


## Technologies
Project was created with:
* Visual Studio 2019 Version 16
* Microsoft .NET Sdk
* .NET Core Framework

## How To Set Up
To clone this GitHub repo and open this project, you will need **Git** and **Visual Studio 2019** installed on your computer:

1. Open Visual Studio 2019.
2. On the start window, select Clone a repository.
3. Enter or type the repository location, and then select Clone.
4. You might be asked for your user sign-in information in the Git User Information dialog box. You can either add your information or edit the default information it provides. Select Save to add the info to your global .gitconfig file. (Or, you can choose to do this later by selecting Cancel.) Next, Visual Studio automatically loads and opens the solution from the repository.
5. To start the program, press the green arrow (Start without debugging button) on the main Visual Studio toolbar, or press Ctrl+F5 to run the program. Visual Studio attempts to build the code in your project and run it. If that succeeds, great! But if not, click on the following link for troubleshooting https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/run-program?view=vs-2019#troubleshooting

Microsoft reference: https://docs.microsoft.com/en-us/visualstudio/get-started/tutorial-open-project-from-repo-visual-studio-2019?view=vs-2019&tabs=vs168later

## Authors

* **Keira Kaitlynn** - https://github.com/keirakaitlynn
* **Jerry Belmonte** - https://github.com/jerrybelmonte
