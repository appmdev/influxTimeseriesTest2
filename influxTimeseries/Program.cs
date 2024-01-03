using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Xml.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("InfluxDB 2.x, World!");
var _token = "jpH4R2GCnJLkQmM15110IbeFqXXMy0ARozSxGlXzLPOyqVA9uv3hXeqBoBGkVUsUcMihxU5kMRiOZPoiRLusIQ==";
var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);

var totalSamplesGlobal = 0;
var totalRecordsGlobal = 0;
var locationn = "location100";
var group = "group-2";

for (int k = 0; k < 150; k++)
{
    for (int j = 0; j < 3; j++)
    {
        var name = "robot" + k.ToString();
        group = "group-1";
        if (j == 0)
        {
            name = "robot" + k.ToString();
            group = "group-1";
        }
        if (j == 1)
        {
            name = "location" + k.ToString();
            group = "group-2";
        }
        if (j == 2)
        {
            name = "netapp" + k.ToString();
            group = "group-3";
        }
        try
        {
            var nameee = name.ToString();
            //var flux2 = "from(bucket: \"test-bucket\")|> range(start: v.timeRangeStart, stop: v.timeRangeStop) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: v.windowPeriod, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
            //|> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\")
            //var flux4 = "from(bucket: \"test-bucket\")|> range(start: v.timeRangeStart, stop: v.timeRangeStop) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: v.windowPeriod, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
            /*

          from(bucket: "test-bucket")
          |> range(start: v.timeRangeStart, stop: v.timeRangeStop)
          |> filter(fn: (r) => r["_measurement"] == "performance")
          |> filter(fn: (r) => r["_field"] == "Disk" or r["_field"] == "RAM" or r["_field"] == "CPU")
          |> filter(fn: (r) => r["location100"] == "group-2")
          |> aggregateWindow(every: 1mo, fn: last, createEmpty: false)
          |> yield(name: "last")
            //*/
            /*
            var fluxAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\")";
            var fluxDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\")";
            var fluxAgrAverageDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 1mo, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
            var fluxAgrMinDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: min, createEmpty: false)  |> yield(name: \"min\")";
            var fluxAgrMaxDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: max, createEmpty: false)  |> yield(name: \"max\")";
            var fluxAgrAverageDiskTotal = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
            var fluxAgrAverageAllLast = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 1mo, fn: last, createEmpty: false)  |> yield(name: \"last\")";
            //*/
            
            var fluxAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\") |> filter(fn: (r) => r[\"" + nameee + "\"] == \"" + group + "\")";
            var fluxDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\""+nameee+ "\"] == \"" + group + "\")";
            var fluxAgrAverageDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"" + nameee + "\"] == \"" + group + "\") |> aggregateWindow(every: 1mo, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
            var fluxAgrMinDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"" + nameee + "\"] == \"" + group + "\") |> aggregateWindow(every: 120s, fn: min, createEmpty: false)  |> yield(name: \"min\")";
            var fluxAgrMaxDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"" + nameee + "\"] == \"" + group + "\") |> aggregateWindow(every: 120s, fn: max, createEmpty: false)  |> yield(name: \"max\")";
            var fluxAgrAverageDiskTotal = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"" + nameee + "\"] == \"" + group + "\") |> aggregateWindow(every: 120s, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
            var fluxAgrAverageAllLast = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"" + nameee + "\"] == \"" + group + "\") |> aggregateWindow(every: 1mo, fn: last, createEmpty: false)  |> yield(name: \"last\")";

            //*/
            //fluxAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\") |> filter(fn: (r) => r[\"robot0\"] == \"group-1\")";

            var fluxTables = await client.GetQueryApi().QueryAsync(fluxAll, "5gera");

            var fluxTablesTotalAll1 = await client.GetQueryApi().QueryAsync(fluxAll, "5gera");
            var fluxTablesTotalAllDisk2 = await client.GetQueryApi().QueryAsync(fluxDiskAll, "5gera");
            var fluxTablesAgrAverageDiskAll3 = await client.GetQueryApi().QueryAsync(fluxAgrAverageDiskAll, "5gera");
            var fluxTablesAgrAverageDisk2Minutes4 = await client.GetQueryApi().QueryAsync(fluxAgrMinDisk2Minutes, "5gera");
            var fluxTablesAgrMinDisk2Minutes5 = await client.GetQueryApi().QueryAsync(fluxAgrMaxDisk2Minutes, "5gera");
            var fluxTablesAgrMaxDisk2Minutes6 = await client.GetQueryApi().QueryAsync(fluxAgrAverageDiskTotal, "5gera");
            var fluxTablesAgrAllLast7 = await client.GetQueryApi().QueryAsync(fluxAgrAverageAllLast, "5gera");

            CreateFolder(nameee);


            string pathTotalAll1 = @"E:\influxResults\" + nameee + @"\" + nameee + "Total.txt";
            string pathTotalAll1a = @"E:\influxResultsAll\" + nameee + "Total.txt";
            string pathTotalDisk2 = @"E:\influxResults\" + nameee + @"\" + nameee + "TotalDisk.txt";
            string pathTotalAvgDisk3 = @"E:\influxResults\" + nameee + @"\" + nameee + "TotalAvgDisk.txt";
            string pathTotalAvgDisk2minutes4 = @"E:\influxResults\" + nameee + @"\" + nameee + "Avg2Minutes.txt";
            string pathTotalMinDisk2minutes5 = @"E:\influxResults\" + nameee + @"\" + nameee + "Minim2Minutes.txt";
            string pathTotalMaxDisk2minutes6 = @"E:\influxResults\" + nameee + @"\" + nameee + "Max2Minutes.txt";
            string pathLast7 = @"E:\influxResults\" + nameee + @"\" + nameee + "Last.txt";
            string pathLast7a = @"E:\influxResultsAll\" + nameee + "Last.txt";
            CreateFileAsync(pathTotalAll1, fluxTablesTotalAll1, nameee, true);
            CreateFileAsync(pathTotalAll1a, fluxTablesTotalAll1, nameee, false);
            CreateFileAsync(pathTotalDisk2, fluxTablesTotalAllDisk2, nameee, false);
            CreateFileAsync(pathTotalAvgDisk3, fluxTablesAgrAverageDiskAll3, nameee , false);
            CreateFileAsync(pathTotalAvgDisk2minutes4, fluxTablesAgrAverageDisk2Minutes4, nameee, false);
            CreateFileAsync(pathTotalMinDisk2minutes5, fluxTablesAgrMinDisk2Minutes5, nameee, false);
            CreateFileAsync(pathTotalMaxDisk2minutes6, fluxTablesAgrMaxDisk2Minutes6, nameee, false);
            CreateFileAsync(pathLast7, fluxTablesAgrAllLast7, nameee, false);
            CreateFileAsync(pathLast7a, fluxTablesAgrAllLast7, nameee, false);
            //Thread.Sleep(200);
            //Task.Delay(200);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        Console.WriteLine("Total samples: " +totalSamplesGlobal.ToString() + "; Total records: " + totalRecordsGlobal + " at: "+ DateTime.Now.ToString());

    }
}


/*

var flux2 = "from(bucket: \"test-bucket\")|> range(start: v.timeRangeStart, stop: v.timeRangeStop) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: v.windowPeriod, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
//|> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\")
//var flux4 = "from(bucket: \"test-bucket\")|> range(start: v.timeRangeStart, stop: v.timeRangeStop) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: v.windowPeriod, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";


var fluxDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\")";
var fluxAgrAverageDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
var fluxAgrMinDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: min, createEmpty: false)  |> yield(name: \"min\")";
var fluxAgrMaxDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: max, createEmpty: false)  |> yield(name: \"max\")";
var fluxAgrAverageDiskTotal = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
var fluxTables2 = await client.GetQueryApi().QueryAsync(fluxDiskAll, "5gera");

//*/

/*
var table0 = fluxTables[0];
var totalll = table0.Records.Count();
Console.WriteLine(totalll );
CreateFileAsync("asd", fluxTables, "" );
foreach (var table in fluxTables)
{
    var fluxRecords = table.Records;
    var countt = table.Records.Count();
    foreach (var fluxRecord in fluxRecords)
    {
        Console.WriteLine($"Field {fluxRecord.GetField()} at {fluxRecord.GetTime()} value: {fluxRecord.GetValue()}");
        // Console.WriteLine($"{fluxRecord.GetTime()}: {fluxRecord.GetValue()}");
        // Console.WriteLine($"Field {fluxRecord.GetField()} at {fluxRecord.GetTime()} value: {fluxRecord.GetValue()}");

    }
}
//*/

/*
var _writeApi = client.GetWriteApi();

var _random = Random.Shared;
var tasks = new List<Task>();
int check = 0;
for (int k = 0; k < 150; k++)
{
    for(int j= 0;j <3; j++)
    {
        var name = "robot" + k.ToString();
        var group = "group-1";
        if (j == 0)
        {
            name = "robot" + k.ToString();
            group = "group-1";
        }
        if(j == 1)
        {
            name = "location" + k.ToString();
            group = "group-2";
        }
        if(j == 2)
        {
            name = "netapp" + k.ToString();
            group = "group-3";
        }


        var producerTask = Task.Run(async () =>
        {
            int i = 0;
            while (true)
            {
                i = i + 1;
                var point = PointData.Measurement("performance")
                    .Tag(name, group)
                    .Field("RAM", _random.Next(100, 8000))
                    .Field("CPU", _random.Next(10, 100))
                    .Field("Disk", _random.Next(100, 8000))
                    .Timestamp(DateTime.UtcNow, WritePrecision.Ns);
                _writeApi.WritePoint(point, "test-bucket", "5gera");
                await Task.Delay(200);            

                check++;
                if (check %1000 == 0)
                {
                    var timenow = DateTime.Now;
                    var timetostring = timenow.ToString();
                    Console.WriteLine("Inserted " + i.ToString() + " at:" + timetostring + " total=  " + check);
                }
            }
        });
        //tasks.Add(producerTask);
    }

}
await Task.WhenAll(tasks);

//*/



async void CreateFiles(string nameee, string group)
{
    //var flux2 = "from(bucket: \"test-bucket\")|> range(start: v.timeRangeStart, stop: v.timeRangeStop) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: v.windowPeriod, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
    //|> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\")
    //var flux4 = "from(bucket: \"test-bucket\")|> range(start: v.timeRangeStart, stop: v.timeRangeStop) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: v.windowPeriod, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
    /*
 
  from(bucket: "test-bucket")
  |> range(start: v.timeRangeStart, stop: v.timeRangeStop)
  |> filter(fn: (r) => r["_measurement"] == "performance")
  |> filter(fn: (r) => r["_field"] == "Disk" or r["_field"] == "RAM" or r["_field"] == "CPU")
  |> filter(fn: (r) => r["location100"] == "group-2")
  |> aggregateWindow(every: 1mo, fn: last, createEmpty: false)
  |> yield(name: "last")
    //*/
    
    var fluxAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\")";
    var fluxDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\")";
    var fluxAgrAverageDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 1mo, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
    var fluxAgrMinDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: min, createEmpty: false)  |> yield(name: \"min\")";
    var fluxAgrMaxDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: max, createEmpty: false)  |> yield(name: \"max\")";
    var fluxAgrAverageDiskTotal = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 120s, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
    var fluxAgrAverageAllLast = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r[\"location100\"] == \"group-2\") |> aggregateWindow(every: 1mo, fn: last, createEmpty: false)  |> yield(name: \"last\")";

    /*
    var fluxAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\" or r[\"_field\"] == \"RAM\" or r[\"_field\"] == \"CPU\") |> filter(fn: (r) => r["+nameee+"] == "+group+")";
    var fluxDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r["+nameee+"] == "+group+")";
    var fluxAgrAverageDiskAll = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r["+nameee+"] == "+group+") |> aggregateWindow(every: 1mo, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
    var fluxAgrMinDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r["+nameee+"] == "+group+") |> aggregateWindow(every: 120s, fn: min, createEmpty: false)  |> yield(name: \"min\")";
    var fluxAgrMaxDisk2Minutes = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r["+nameee+"] == "+group+") |> aggregateWindow(every: 120s, fn: max, createEmpty: false)  |> yield(name: \"max\")";
    var fluxAgrAverageDiskTotal = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r["+nameee+"] == "+group+") |> aggregateWindow(every: 120s, fn: mean, createEmpty: false)  |> yield(name: \"mean\")";
    var fluxAgrAverageAllLast = "from(bucket: \"test-bucket\")|> range(start: 0) |> filter(fn: (r) => r[\"_measurement\"] == \"performance\") |> filter(fn: (r) => r[\"_field\"] == \"Disk\") |> filter(fn: (r) => r["+nameee+"] == "+group+") |> aggregateWindow(every: 1mo, fn: last, createEmpty: false)  |> yield(name: \"last\")";

    //*/

    var fluxTables = await client.GetQueryApi().QueryAsync(fluxAll, "5gera");

    var fluxTablesTotalAll1 = await client.GetQueryApi().QueryAsync(fluxAll, "5gera");
    var fluxTablesTotalAllDisk2 = await client.GetQueryApi().QueryAsync(fluxDiskAll, "5gera");
    var fluxTablesAgrAverageDiskAll3 = await client.GetQueryApi().QueryAsync(fluxAgrAverageDiskAll, "5gera");
    var fluxTablesAgrAverageDisk2Minutes4 = await client.GetQueryApi().QueryAsync(fluxAgrMinDisk2Minutes, "5gera");
    var fluxTablesAgrMinDisk2Minutes5 = await client.GetQueryApi().QueryAsync(fluxAgrMaxDisk2Minutes, "5gera");
    var fluxTablesAgrMaxDisk2Minutes6 = await client.GetQueryApi().QueryAsync(fluxAgrAverageDiskTotal, "5gera");
    var fluxTablesAgrAllLast7 = await client.GetQueryApi().QueryAsync(fluxAgrAverageAllLast, "5gera");

    CreateFolder(nameee);


    string pathTotalAll1 = @"E:\influxResults\" + nameee + @"\" + nameee + "Total.txt";
    string pathTotalAll1a = @"E:\influxResultsAll\" + nameee + @"\" + nameee + "Total.txt";
    string pathTotalDisk2 = @"E:\influxResults\" + nameee + @"\" + nameee + "TotalDisk.txt";
    string pathTotalAvgDisk3 = @"E:\influxResults\" + nameee + @"\" + nameee + "TotalAvgDisk.txt";
    string pathTotalAvgDisk2minutes4 = @"E:\influxResults\" + nameee + @"\" + nameee + "Avg2Minutes.txt";
    string pathTotalMinDisk2minutes5 = @"E:\influxResults\" + nameee + @"\" + nameee + "Minim2Minutes.txt";
    string pathTotalMaxDisk2minutes6 = @"E:\influxResults\" + nameee + @"\" + nameee + "Max2Minutes.txt";
    string pathLast7 = @"E:\influxResults\" + nameee + @"\" + nameee + "Last.txt";
    string pathLast7a = @"E:\influxResultsAll\" + nameee + @"\" + nameee + "Last.txt";
    CreateFileAsync(pathTotalAll1, fluxTablesTotalAll1, nameee, true);
    CreateFileAsync(pathTotalAll1a, fluxTablesTotalAll1, nameee, false);
    CreateFileAsync(pathTotalDisk2, fluxTablesTotalAllDisk2, nameee,false);
    CreateFileAsync(pathTotalAvgDisk3, fluxTablesAgrAverageDiskAll3, nameee, false);
    CreateFileAsync(pathTotalAvgDisk2minutes4, fluxTablesAgrAverageDisk2Minutes4, nameee, false);
    CreateFileAsync(pathTotalMinDisk2minutes5, fluxTablesAgrMinDisk2Minutes5, nameee, false);
    CreateFileAsync(pathTotalMaxDisk2minutes6, fluxTablesAgrMaxDisk2Minutes6, nameee, false);
    CreateFileAsync(pathLast7, fluxTablesAgrAllLast7, nameee, false);
    CreateFileAsync(pathLast7a, fluxTablesAgrAllLast7, nameee, false);
}


async void CreateFileAsync(string fileName, IEnumerable<FluxTable> fluxTables, string objectNamee, bool count)
{
    try
    {
        // Check if file already exists. If yes, delete it.
        if (System.IO.File.Exists(fileName))
        {
            System.IO.File.Delete(fileName);
        }

        // Create a new file
        using (StreamWriter sw = System.IO.File.CreateText(fileName))
        {
            if(count)
            {
                var totalSamples = fluxTables.ElementAt(0).Records.Count();
                totalSamplesGlobal = totalSamplesGlobal + totalSamples;
            }
            //var table00 = fluxTables[0];
            await sw.WriteLineAsync(" ");
            await sw.WriteLineAsync(" ");
            foreach (var table in fluxTables)
            {
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");

                var fluxRecords = table.Records;

                var totalResultsNr = table.Records.Count();
                if (count)
                {
                    totalRecordsGlobal = totalRecordsGlobal + totalResultsNr;
                }
                await sw.WriteLineAsync("total results for " + objectNamee + " = " + totalResultsNr.ToString());

                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                await sw.WriteLineAsync(" ");
                int k = 0;
                foreach (var fluxRecord in fluxRecords)
                {
                    var fieldNamee = fluxRecord.GetField().ToString();
                    var timee = fluxRecord.GetTime().ToString();
                    var valuee = fluxRecord.GetValue().ToString();

                    var roww = k.ToString() + "- Field " + fieldNamee + " at "+ timee + " value: " + valuee;

                    await sw.WriteLineAsync(roww);
                    k++;
                }
            }            
        }
    }
    catch (Exception Ex)
    {
        Console.WriteLine(Ex.ToString());
    }
}

void CreateFolder(string folderName)
{
    try
    {
        string path = @"E:\influxResults\" + folderName;
        // Determine whether the directory exists.
        if (Directory.Exists(path))
        {
            //Console.WriteLine("That path exists already.");
            return;
        }
        // Try to create the directory.
        DirectoryInfo di = Directory.CreateDirectory(path);
        //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
    }
    catch (Exception e)
    {
        Console.WriteLine("The process failed: {0}", e.ToString());
    }
    finally { }
}
/*
async void CreateFileAsync(string fileName, IReadOnlyList<TimeSeriesTuple> results, string objectNamee, bool count)
{
    try
    {
        // Check if file already exists. If yes, delete it.
        if (System.IO.File.Exists(fileName))
        {
            System.IO.File.Delete(fileName);
        }

        // Create a new file
        using (StreamWriter sw = System.IO.File.CreateText(fileName))
        {
            var totalResultsNr = results.Count;
            if (count)
            {
                totalll = totalll + totalResultsNr;
                Console.WriteLine("Total: " + totalll);
            }
            int k = 0;
            await sw.WriteLineAsync("total results for " + objectNamee + "= " + totalResultsNr.ToString());
            foreach (TimeSeriesTuple result in results)
            {
                var time = result.Time.Value.ToString();
                var value = result.Val.ToString();
                var roww = k.ToString() + "- " + time + ": " + value;
                await sw.WriteLineAsync(roww);
                k++;
            }
        }
    }
    catch (Exception Ex)
    {
        Console.WriteLine(Ex.ToString());
    }
}
//*/