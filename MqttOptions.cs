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