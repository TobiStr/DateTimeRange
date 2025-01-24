# DateTimeRange

[![NuGet](https://img.shields.io/nuget/dt/DateRange)](https://www.nuget.org/packages/DateRange)
[![NuGet](https://img.shields.io/nuget/vpre/DateRange)](https://www.nuget.org/packages/DateRange)

Provides a DateTimeRange type with enumerator for better handling of DateTime ranges in .NET.

## Add Nuget-Package

You should install [DateRange with NuGet](https://www.nuget.org/packages/DateRange):

    Install-Package DateRange

Or via the .NET Core command line interface:

    dotnet add package DateRange

Either commands, from Package Manager Console or .NET Core CLI, will download and install DateRange.
DateRange is compatible with .NET 4.6.1+, NET Standard, NET Core and all .NET versions above.

## Usage

Import System:
```csharp
  using System;
```

### Initialize:
```csharp
  //With start and end date
  var b = new DateTimeRange(DateTime.Now, DateTime.Now.AddMonths(3));
  
  //With start date and duration
  var c = new DateTimeRange(DateTime.Now, new TimeSpan(100, 0, 0, 0));
  
  //With end date and duration
  var d = new DateTimeRange(new TimeSpan(100, 0, 0, 0), DateTime.Now);
```

### Enumerate:
Enumerate DateTimes inside of the DateTimeRange in custom steps.  StartDate and EndDate are optionally excludable.
```csharp
  //Using the custom DateSpan Enum
  DateSpan dateSpan = DateSpan.Day;
  var result = dateTimeRange.Enumerate(dateSpan).ToArray();
  var result = dateTimeRange.Enumerate(dateSpan, excludeStart: true, excludeEnd: true).ToArray();
  
  //Using TimeSpan
  TimeSpan step = new TimeSpan(1,0,0,0);
  var result = dateTimeRange.Enumerate(step).ToArray();
  var result = dateTimeRange.Enumerate(step, excludeStart: true, excludeEnd: true).ToArray();
```

### Contains:
Check if a DateTime is inside of a DateTimeRange. StartDate and EndDate are optionally excludable.
```csharp
  // Declare DateTimeRange and DateTime to check for
  DateTimeRange dateTimeRange = new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(2));
  DateTime dateTime = DateTime.Now.AddDay(1);

  // Check if DateTime is inside of DateTimeRange
  var result = dateTimeRange.Contains(dateTime);
  var result = dateTimeRange.Contains(dateTime, excludeStart: true, excludeEnd: true);

  Assert.That(result);
```

### Intersect:
Combine two DateTimeRanges into one. The new DateTimeRange will range from the earliest StartDate to the latest EndDate.
```csharp
  //Declare two DateTimeRanges
  var a = DateTime.Now;
  var b = a.AddDays(-1);
  var c = a.AddDays(1);
  DateTimeRange first = new DateTimeRange(b, a);
  DateTimeRange second = new DateTimeRange(a, c);

  //Intersect
  var result = first.Intersect(second);

  Assert.That(result.Start == b && result.End == c);
```


### P.S.:
Let me know, if you like to have more features. 