using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Objects
{
    public class PushNotificationFilters
    {

        public List<string> custom_id { get; set; }
    }
    //=============================================
    public class PushNotificationData
    {
        public string title { get; set; }
        public string content { get; set; }
    }
    //=============================================
    public class PushNotificationWithFilters
    {
        public string app_ids { get; set; }
        public PushNotificationFilters filters { get; set; }
        public PushNotificationData data { get; set; }
    }
    //=============================================
    public class PushNotifications
    {
        public string app_ids { get; set; }
        public PushNotificationData data { get; set; }
        public List<string> topics { get; set; }
    }
    //=============================================
}
