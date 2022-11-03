namespace CF.Identity.API.Settings
{
    public class EventBusSettings
    {
        public string HostAddress { get; set; }

        public EventBusSettings()
        {

        }

        public EventBusSettings(string hostAddress)
        {
            HostAddress = hostAddress;
        }
    }
}
