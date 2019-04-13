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
        private static async Task SendNotificationToCurrentMobileClientAsync(List<Notification> notifications, IFirebaseTokenService _firebaseTokenService)
        {
            foreach (var notification in notifications)
            {
                var fbtokens = _firebaseTokenService.GetFirebaseTokens(_ => _.UserId == notification.UserId).ToList();
                foreach (var ft in fbtokens)
                {
                    var fb = new FirebaseNotification()
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
                    };
                    await SendFirebaseNotification.SendNotificationToMobileAsync(fb);
                }

                //.ForEach(async ft =>
                //                {
                //                    await SendFirebaseNotification.SendNotificationToMobileAsync(new FirebaseNotification()
                //                    {
                //                        to = ft.Token,
                //                        notification = new NotificationObject()
                //                        {
                //                            title = notification.Title,
                //                            body = notification.Body,
                //                        },
                //                        data = new Dictionary<string, string>()
                //                        {
                //            { "ReceivableList", notification.NData }
                //                        }
                //                    });
                //                });
            }
            //notifications.ForEach(notification =>
            //{

            //});
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

        private static async Task SendNotificationToCurrentMobileClientAsync(Notification notification, IFirebaseTokenService _firebaseTokenService)
        {

            var tokens = _firebaseTokenService.GetFirebaseTokens(_ => _.UserId == notification.UserId).ToList();
            foreach (var ft in tokens)
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
            }

        }

        private static async Task SendNotificationToCurrentWebClient(Notification notification, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext)
        {

            var connections = _hubService.GetHubUserConnections(_ => _.UserId.Equals(notification.UserId));
            await _hubContext.Clients.Clients(connections.Select(_ => _.Connection).ToList())
                         .SendAsync("Notify", JsonConvert.SerializeObject(notification.Adapt<NotificationVM>()));

        }

        public static async Task SendNotification(Notification notification, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext, IFirebaseTokenService _firebaseTokenService)
        {
            await SendNotificationToCurrentMobileClientAsync(notification, _firebaseTokenService);
            await SendNotificationToCurrentWebClient(notification, _hubService, _hubContext);
        }

        public static async Task SendNotification(List<Notification> notifications, IHubUserConnectionService _hubService, IHubContext<CenterHub> _hubContext, IFirebaseTokenService _firebaseTokenService)
        {
            await SendNotificationToCurrentMobileClientAsync(notifications, _firebaseTokenService);
            SendNotificationToCurrentWebClient(notifications, _hubService, _hubContext);
        }
    }
}
