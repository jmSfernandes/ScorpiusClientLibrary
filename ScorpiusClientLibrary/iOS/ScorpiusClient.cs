using System;
using System.Collections.Generic;
using Firebase.CloudMessaging;
using Firebase.InstanceID;
using UIKit;

namespace ScorpiusClientLibrary
{
    public class ScorpiusClientImplementation : IScorpiusClient
    {
        public void Init(object receiver)
        {
            try
            {
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
                Firebase.Core.App.Configure();
                Messaging.SharedInstance.Delegate = (IMessagingDelegate) receiver;
                InstanceId.Notifications.ObserveTokenRefresh((sender, e) =>
                {
                    InstanceId.SharedInstance.GetInstanceId((result, error) =>
                    {
                        if (error == null)
                        {
                            string token = result.Token;
                            Console.WriteLine("Got a notification token: " + token);
                            //do something with token
                        }
                        else
                        {
                            Console.WriteLine("couldn't get Firebase Token: " + error);
                        }
                    });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void SetCallback(Action<object> Callback)
        {
            throw new NotImplementedException();
        }

        public void SubscribeToMultipleTopics(IEnumerable<string> courses)
        {
            foreach (var id in courses)
            {
                Messaging.SharedInstance.Subscribe(id);
                Console.WriteLine("Subscribed to topic: " + id);
            }
        }

        public void UnSubscribeFromMultipleTopics(IEnumerable<string> courses)
        {
            foreach (var id in courses)
            {
                Messaging.SharedInstance.Unsubscribe(id);
                Console.WriteLine("Unsubscribed to topic: " + id);
            }
        }

        public void UnSubscribeFromSingleTopic(string topic)
        {
            Messaging.SharedInstance.Unsubscribe(topic);
            Console.WriteLine("Unsubscribed to topic: " + topic);
        }

        public void SubscribeToSingleTopic(string topic)
        {
            Messaging.SharedInstance.Subscribe(topic);
            Console.WriteLine("Subscribed to topic: " + topic);
        }
    }
}