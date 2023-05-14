using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Util;
using Firebase;
using Firebase.Messaging;

namespace ScorpiusClientLibrary
{
    public class ScorpiusClientImplementation : IScorpiusClient
    {
        private const string Tag = "TopicSubscriptionService";


        public void Init(object context)
        {
            if (!IsPlayServicesAvailable((Context) context))
            {
                GoogleApiAvailability.Instance.MakeGooglePlayServicesAvailable((Activity) context);
            }

            FirebaseApp.InitializeApp((Context) context);
        }

        public void SetCallback(Action<object> Callback)
        {
            FirebaseMessagingReceiverService.CallbackFunction = Callback;
        }

        public void SubscribeToMultipleTopics(IEnumerable<string> topics)
        {
            foreach (var topic in topics)
            {
                SubscribeToSingleTopic(topic);
            }
        }

        public void UnSubscribeFromMultipleTopics(IEnumerable<string> topics)
        {
            foreach (var topic in topics)
            {
                UnSubscribeFromSingleTopic(topic);
            }
        }

        public void UnSubscribeFromSingleTopic(string topic)
        {
            try
            {
                FirebaseMessaging.Instance.UnsubscribeFromTopic(topic);

                Log.Info(Tag, "Unsubscribed from topic: " + topic);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Unsubscribing: {0}", ex.Message);
            }
        }

        public void SubscribeToSingleTopic(string topic)
        {
            try
            {
                FirebaseMessaging.Instance.SubscribeToTopic(topic);
                Log.Info(Tag, "Subscribed to new topic: " + topic);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while subscribing: {0}", ex.Message);
            }
        }

        private bool IsPlayServicesAvailable(Context context)
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Console.WriteLine("This device is not supported.");
                    throw new Exception("Device is not supported");
                }

                return false;
            }

            Console.WriteLine("Google Play Services is available.");
            return true;
        }
    }
}