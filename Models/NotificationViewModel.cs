namespace Notification.Models
{
    public class NotificationViewModel
    {
        public string Title { get; set; }
        public List<string> Channels { get; set; }

        public NotificationViewModel()
        {
            Channels = new List<string>();
        }
    }
}
