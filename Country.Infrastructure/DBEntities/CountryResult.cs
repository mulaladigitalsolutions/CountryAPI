using System;
using System.Collections.Generic;
using System.Text.Json;

public class NameInfo
{
    public string Common { get; set; }
    public string Official { get; set; }
}

public class CountryInfo
{
    public NameInfo Name { get; set; }
    public string Cca2 { get; set; }
    public string Cca3 { get; set; }
    public List<string> Capital { get; set; }
    public string Region { get; set; }
    public double Area { get; set; }
    public int Population { get; set; }
    public Dictionary<string, CurrencyInfo> Currencies { get; set; }
    public List<string> Timezones { get; set; }
    public Dictionary<string, string> Flags { get; set; }
}

public class CurrencyInfo
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}