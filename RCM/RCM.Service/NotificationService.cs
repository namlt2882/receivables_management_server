using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetNotifications();
        IEnumerable<Notification> GetNotifications(Expression<Func<Notification, bool>> where);
        Notification GetNotification(int id);
        void CreateNotification(Notification notification);
        void EditNotification(Notification notification);
        void RemoveNotification(int id);
        void RemoveNotification(Notification notification);
        void SaveNotification();
    }

    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateNotification(Notification notification)
        {
            notification.CreatedDate = DateTime.Now;
            _notificationRepository.Add(notification);
        }

        public void EditNotification(Notification notification)
        {
            var entity = _notificationRepository.GetById(notification.Id);
            entity = notification;
            entity.UpdatedDate = DateTime.Now;
            _notificationRepository.Update(entity);
        }

        public Notification GetNotification(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _notificationRepository.GetAll();
        }

        public IEnumerable<Notification> GetNotifications(Expression<Func<Notification, bool>> where)
        {
            return _notificationRepository.GetMany(where);
        }

        public void RemoveNotification(int id)
        {
            var entity = _notificationRepository.GetById(id);
            _notificationRepository.Delete(entity);
        }

        public void RemoveNotification(Notification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public void SaveNotification()
        {
            _unitOfWork.Commit();
        }
    }
}
