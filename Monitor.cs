/*
    MIT License

    Copyright (c) 2022 Steve Morgan

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using System.Security.Authentication;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;

public class Monitor : BackgroundService
{
    private InMqttOptions _inMqttOptions;
    private OutMqttOptions _outMqttOptions;
    private ILogger<Monitor> _logger;

    public Monitor(IOptions<InMqttOptions> inMqttOptions, IOptions<OutMqttOptions> outMqttOptions, ILogger<Monitor> logger)
    {
        _inMqttOptions = inMqttOptions.Value;
        _outMqttOptions = outMqttOptions.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var inClient = new MqttFactory().CreateMqttClient();
        var outClient = new MqttFactory().CreateMqttClient();

        inClient.UseConnectedHandler(async e =>
        {
            Console.WriteLine($"Connected to {_inMqttOptions.Broker}");
            await inClient.SubscribeAsync(_inMqttOptions.Topic);
        });

        outClient.UseConnectedHandler(async e =>
        {
            Console.WriteLine($"Connected to {_outMqttOptions.Broker}");
            await Task.Delay(0);
        });

        inClient.UseDisconnectedHandler(async e =>
        {
            Console.WriteLine($"Disconnected from {_inMqttOptions.Broker}");
            await Task.Delay(TimeSpan.FromSeconds(5));
            await inClient.ConnectAsync(new MqttClientOptionsBuilder()
                .WithClientId("mqtt-monitor")
                .WithTcpServer(_inMqttOptions.Broker, _inMqttOptions.Port)
                .WithCredentials(_inMqttOptions.Username, _inMqttOptions.Password)
                .WithTls(
                    o =>
                    {
                        o.SslProtocol = SslProtocols.Tls12; // The default value is determined by the OS. Set manually to force version.
                    })
                .Build());
        });

        outClient.UseDisconnectedHandler(async e =>
        {
            Console.WriteLine($"Disconnected from {_outMqttOptions.Broker}");
            await Task.Delay(TimeSpan.FromSeconds(5));
            await inClient.ConnectAsync(new MqttClientOptionsBuilder()
                .WithClientId("mqtt-monitor")
                .WithTcpServer(_outMqttOptions.Broker, _outMqttOptions.Port)
                .Build());
        });

        inClient.UseApplicationMessageReceivedHandler(async e =>
        {
            // Console.WriteLine($"{e.ApplicationMessage.Topic} {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            try
            {
                var message = GetMeterMessage(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

                if(!string.IsNullOrWhiteSpace(message))
                {
                    await WaitForClient(outClient);

                    var outMessage = new MqttApplicationMessageBuilder()
                        .WithTopic(_outMqttOptions.Topic)
                        .WithPayload(message)
                        .WithExactlyOnceQoS()
                        .WithRetainFlag()
                        .Build();

                    await outClient.PublishAsync(outMessage);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        });

        await inClient.ConnectAsync(new MqttClientOptionsBuilder()
            .WithClientId("mqtt-monitor")
            .WithTcpServer(_inMqttOptions.Broker, _inMqttOptions.Port)
            .WithCredentials(_inMqttOptions.Username, _inMqttOptions.Password)
            .WithTls(
                o =>
                {
                    o.SslProtocol = SslProtocols.Tls12; // The default value is determined by the OS. Set manually to force version.
                })
            .Build());

        await outClient.ConnectAsync(new MqttClientOptionsBuilder()
            .WithClientId("mqtt-monitor")
            .WithTcpServer(_outMqttOptions.Broker, _outMqttOptions.Port)
            .Build());
    }

    private async Task WaitForClient(IMqttClient? client)
    {
        if(client == null) return;

        var retryCount = 0;

        while(!client.IsConnected && retryCount++ < 10)
        {
            Console.WriteLine($"Waiting for client to connect...");
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private string GetMeterMessage(string inputPayload)
    {
        var glowMessage = GlowMqttMessage.FromJson(inputPayload);
        var outMessage = OutgoingMeteringMessage.FromGlowMqttMessage(glowMessage);
        return outMessage?.ToJson() ?? string.Empty;
    }
}