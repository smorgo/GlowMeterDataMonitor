// See https://aka.ms/new-console-template for more information
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("secrets.json", optional: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configurationRoot = hostContext.Configuration;
        services.Configure<InMqttOptions>(
            configurationRoot.GetSection("InMqtt"));
        services.Configure<OutMqttOptions>(
            configurationRoot.GetSection("OutMqtt"));
        services.AddHostedService<Monitor>();
    })
    .Build();

Console.WriteLine("Glow Meter Data Monitor");

await host.RunAsync();

