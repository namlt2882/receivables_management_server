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
        public static void SendNotificationToCurrentMobileClient(List<Notification> notifications, IFirebaseTokenService _firebaseTokenService)
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

        public static void SendNotificationToCurrentWebClient(List<Notification> notifications, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext)
        {
            notifications.ForEach(async notification =>
            {
                var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(notification.UserId));
                await _hubContext.Clients.Clients(connections.Select(_ => _.Connection).ToList())
                            .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));
            });
        }

        public static void SendNotificationToCurrentMobileClient(Notification notification, IFirebaseTokenService _firebaseTokenService)
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

        public static async Task SendNotificationToCurrentWebClient(Notification notification, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext)
        {
            var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(notification.UserId));
            await _hubContext.Clients.Clients(connections.Select(_ => _.Connection).ToList())
                        .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));
        }
    }
}
