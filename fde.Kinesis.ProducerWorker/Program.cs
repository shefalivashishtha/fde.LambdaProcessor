﻿namespace fde.Kinesis.ProducerWorker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<ProducerWorker>();

        var host = builder.Build();
        host.Run();
    }
}
