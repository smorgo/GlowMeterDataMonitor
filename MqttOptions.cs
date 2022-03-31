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

public class MqttOptions
{
    // Trim these values to avoid issues to Kubernetes secrets.

    private string _broker = "mqtt.smorgo.com";
    public string Broker 
    {
        get => _broker; 
        set => _broker = value.Trim();
    }
    public int Port {get; set;}
    private string _topic = "#";
    public string Topic 
    {
        get => _topic; 
        set => _topic = value.Trim();
    }
    private string _username = "";
    public string Username 
    {
        get => _username; 
        set => _username = value.Trim();
    }
    private string _password = "";
    public string Password
    {
        get => _password; 
        set => _password = value.Trim();
    }
}