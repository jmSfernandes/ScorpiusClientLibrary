using System;
using System.Collections.Generic;

namespace ScorpiusClientLibrary
{
    public interface IScorpiusClient
    {
        void Init(object context);
        void SetCallback(Action<object> Callback);
        void SubscribeToMultipleTopics(IEnumerable<string> topics);
        void SubscribeToSingleTopic(string topic);

        void UnSubscribeFromMultipleTopics(IEnumerable<string> topics);

        void UnSubscribeFromSingleTopic(string topic);
    }
}