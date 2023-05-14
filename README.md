# ScorpiusClientLibrary

This library deals with platform specific code to subscribe and receive Firebase notifications through specific topics.
This can be used in client applications, in cooperation with the [ScorpiusGE](https://github.com/jmSfernandes/ScorpiusGE).

Check the [ScorpiusClient Example App](https://github.com/jmSfernandes/ScorpiusClient) to get started or to see details on how to implement your code. 

You should also check the firebase documentation for more instructions on how to configure your project.

## Init Library

The Firebase Cloud Messaging system requires some initialization code.Initializing  of SocialSensors must be done in each platform specific code.
This calls must be called in the platform specific code, because it differs for each platform.

### Android

In the Android application we must call the following lines in the MainActivity.cs file.

````csharp
  CrossScorpiusClient.Current.Init(this);
  CrossScorpiusClient.Current.SetCallback((remoteMessage) => {
     
    var remoteMessage = message as RemoteMessage;
    //your logic here code here 
  });

````

you can also encapsulate the logic of the receiver into a different class and pass a method as the callback:

````csharp

  public class MyMessageReceiver
    {
        //private const string NewsChannelId = "1";
        //private const string NewsChannelDescription = "Alert";
        //private readonly long[] _vibrationPattern = {500, 500, 500, 500, 500, 500, 500, 500, 500};
        //private NotificationManager _notificationManager;
        private const string Tag = "MyFirebaseMessagingService";


        public static void OnMessageReceived(object message)
        {
            var remoteMessage = message as RemoteMessage;

           // my logic here
    
       }
   }
   
   ...
  
  CrossScorpiusClient.Current.SetCallback(MyMessageReceiver.OnMessageReceive);



````

You should also add the following lines to your manifest.xml

````xml
  <application android:label="MyAppName.Droid">
  
        <receiver android:name="com.google.firebase.iid.FirebaseInstanceIdInternalReceiver" android:exported="false"/>
        <receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" android:exported="true"
                  android:permission="com.google.android.c2dm.permission.SEND">
            <intent-filter>
                <action android:name="com.google.android.c2dm.intent.RECEIVE"/>
                <action android:name="com.google.android.c2dm.intent.REGISTRATION"/>
                <category android:name="${applicationId}"/>
            </intent-filter>
        </receiver>
    </application>

````


### iOS

On iOS is not possible to encapsulate the logic of the receiver, so instead of the setCallback method, you should implement the IMessagingDelegate on your AppDelegate app and implement your code logic there.  
Like shown below

```csharp
...
    [Register("AppDelegate")]
      public partial class AppDelegate : FormsApplicationDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
       

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            LoadApplication(new App());
            
            CrossScorpiusClient.Current.Init(this);
            
            return base.FinishedLaunching(app, options);
        }
        
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            base.DidReceiveRemoteNotification(application, userInfo, completionHandler);
            //your code here...
            
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            base.FailedToRegisterForRemoteNotifications(application, error);
        }
    }

```

You should also add the following information to your info.plist file
````xml
<key>UIBackgroundModes</key>
<array>
<string>fetch</string>
<string>remote-notification</string>
<string>processing</string>
</array>

````


## Use the library to subscribe to topics

````csharp
        var topic ="my_topic_name"; 
        CrossScorpiusClient.Current.SubscribeToSingleTopic(topic);
        
        //library can be use to iterate over a list of topics
        var topics = new List<string>{"first_topic", "second_topic"};
        CrossScorpiusClient.Current.SubscribeToMultipleTopics(topics);
        
        // you can also unsubscribe to one or multiple topics
        CrossScorpiusClient.Current.UnSubscribeFromSingleTopics(topic);
        CrossScorpiusClient.Current.UnSubscribeFromMultipleTopics(topics);
        
````

## LICENSE
MIT License

Copyright (c) 2023 J. Fernandes

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
