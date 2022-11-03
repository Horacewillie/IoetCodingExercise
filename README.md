<!-- PROJECT LOGO -->
<br />
<p align="center">

<h3 align="center">IOET CODING EXERCISE</h3>

  <p align="center">
    Exercise to calculate payments by schedule and fees 
    <br /> 
    <br />
    </p>

## About The Project

The company ACME offers their employees the flexibility to work the hours they want. They will pay for the hours worked based on the day of the week and time of day, according to the following table:

Monday - Friday

00:01 - 09:00 25 USD

09:01 - 18:00 15 USD

18:01 - 00:00 20 USD

Saturday and Sunday

00:01 - 09:00 30 USD

09:01 - 18:00 20 USD

18:01 - 00:00 25 USD

The goal of this exercise is to calculate the total that the company has to pay an employee, based on the hours they worked and the times during which they worked. The following abbreviations will be used for entering data:

MO: Monday

TU: Tuesday

WE: Wednesday

TH: Thursday

FR: Friday

SA: Saturday

SU: Sunday

### Built With

This project was built it with:

- [MSTEST](https://github.com/microsoft/testfx)
- [Moq](https://github.com/moq/moq4)
- [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)


## Getting Started

### Prerequisites

- Microsoft .NET Framework

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/horacewillie/IoetCodingExercise
   ```
2. Install packages
   ```sh
   dotnet restore
   ```
3. Build
   ```sh
   dotnet build
   ```

## SideNote
There is a .txt file (./Resource/EmployeesTestData.txt) that contains data. You can decide to create yours or add more datasets.

## Solution

The architecture imployed for this project is a microservice architecture, the code contains 4 projects inclusive of the test project depicting the seperation of

different services as its done in a microservice architectured codebase. Solid principles are applied through out and also to prevent tight coupling interfaces were used.

The core solution was arrived at by first studying the 2 test data set given. I noticed that weekend bonus was 5 dollars.

The data being housed in a text file was read using System.IO methods, iterated through and then splitted to obtain the relevant details (Day of the week, Name of Employee and  Time Intervals). Static Helper methods was created to handle the time interval calculation.

The main result was kept in a Dictionary with key of type string for the name and value of type decimal for the pay.

After which, a simple iteration of  the Dictioanry object was done to extract the key and value appropriately.

## Contact Me

Horace Saturday- horacewillie7@gmail.com
