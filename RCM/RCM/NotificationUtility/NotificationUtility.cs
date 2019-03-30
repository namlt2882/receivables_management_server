using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RCM.CenterHubs;
using RCM.Firebase;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;

namespace RCM.NotificationUtility
{
    public static class NotificationUtility
    {
        private static void SendNotificationToCurrentMobileClient(List<Notification> notifications, IFirebaseTokenService _firebaseTokenService)
        {
            notifications.ForEach(async notification =>
            {
                _firebaseTokenService.GetFirebaseTokens(_ => _.UserId == notification.UserId).ToList().ForEach(async ft =>
                {
                    await SendFirebaseNotification.SendNotificationToMobileAsync(new FirebaseNotification()
                    {
                        to = ft.Token,
                        notification = new NotificationObject()
                        {
                            title = notification.Title,
                            body = notification.Body,
                        },
                        data = new Dictionary<string, string>()
                        {
                            { "ReceivableList", notification.NData }
                        }
                    });
                });
            });
        }

        private static void SendNotificationToCurrentWebClient(List<Notification> notifications, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext)
        {
            notifications.ForEach(async notification =>
            {
                var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(notification.UserId));
                await _hubContext.Clients.Clients(connections.Select(_ => _.Connection).ToList())
                            .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));
            });
        }

        private static void SendNotificationToCurrentMobileClient(Notification notification, IFirebaseTokenService _firebaseTokenService)
        {

            _firebaseTokenService.GetFirebaseTokens(_ => _.UserId == notification.UserId).ToList().ForEach(async ft =>
            {
                await SendFirebaseNotification.SendNotificationToMobileAsync(new FirebaseNotification()
                {
                    to = ft.Token,
                    notification = new NotificationObject()
                    {
                        title = notification.Title,
                        body = notification.Body,
                    },
                    data = new Dictionary<string, string>()
                    {
                            { "ReceivableList", notification.NData }
                    }
                });
            });
        }

        private static async Task SendNotificationToCurrentWebClient(Notification notification, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext)
        {
            
            var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(notification.UserId));
            await _hubContext.Clients.Clients(connections.Select(_ => _.Connection).ToList())
                         .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));
            
        }

        public static async Task SendNotification(Notification notification, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext, IFirebaseTokenService _firebaseTokenService)
        {
            SendNotificationToCurrentMobileClient(notification, _firebaseTokenService);
            await SendNotificationToCurrentWebClient(notification, _hubService, _hubContext);
        }

        public static void SendNotification(List<Notification> notifications, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext, IFirebaseTokenService _firebaseTokenService)
        {
            SendNotificationToCurrentMobileClient(notifications, _firebaseTokenService);
            SendNotificationToCurrentWebClient(notifications, _hubService, _hubContext);
        }
    }
}
