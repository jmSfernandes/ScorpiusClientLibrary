using System;
using Android.App;
using Android.Util;
using Firebase.Messaging;

namespace ScorpiusClientLibrary
{
    [Service]
    [IntentFilter(new[] {"com.google.firebase.MESSAGING_EVENT"})]
    public class FirebaseMessagingReceiverService : FirebaseMessagingService
    {
        private const string Tag = "MyFirebaseMessagingService";

        public static Action<object> CallbackFunction { get; set; }

        public override void OnNewToken(string newToken)
        {
            base.OnNewToken(newToken);

            Log.Info(Tag, "Firebase Token: " + newToken);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            Log.Debug(Tag, "From:    " + remoteMessage.From);
            if (CallbackFunction != null)
                CallbackFunction(remoteMessage);
        }
    }
}