# DateTimeRange
Provides a DateTimeRange type with enumerator for better handling of DateTime ranges in .Net.

## Usage

Import System:
```csharp
  using System;
```

Initialize:
```csharp
  //With start and end date
  var b = new DateTimeRange(DateTime.Now, DateTime.Now.AddMonths(3));
  
  //With start date and duration
  var c = new DateTimeRange(DateTime.Now, new TimeSpan(100, 0, 0, 0));
  
  //With end date and duration
  var d = new DateTimeRange(new TimeSpan(100, 0, 0, 0), DateTime.Now);
```

Enumerate:
```csharp
  //Using the custom DateSpan Enum
  DateSpan dateSpan = DateSpan.Day;
  var result = dateTimeRange.Enumerate(dateSpan).ToArray();
  
  //Using TimeSpan
  TimeSpan step = new TimeSpan(1,0,0,0);
  var result = dateTimeRange.Enumerate(step).ToArray();
```
