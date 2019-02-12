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
        void CreateNotification(Notification Notification);
        void EditNotification(Notification Notification);
        void RemoveNotification(int id);
        void RemoveNotification(Notification Notification);
        void SaveNotification();
    }

    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _NotificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository NotificationRepository, IUnitOfWork unitOfWork)
        {
            this._NotificationRepository = NotificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateNotification(Notification Notification)
        {
            _NotificationRepository.Add(Notification);
        }

        public void EditNotification(Notification Notification)
        {
            var entity = _NotificationRepository.GetById(Notification.Id);
            entity = Notification;
            _NotificationRepository.Update(entity);
        }

        public Notification GetNotification(int id)
        {
            return _NotificationRepository.GetById(id);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _NotificationRepository.GetAll();
        }

        public IEnumerable<Notification> GetNotifications(Expression<Func<Notification, bool>> where)
        {
            return _NotificationRepository.GetMany(where);
        }

        public void RemoveNotification(int id)
        {
            var entity = _NotificationRepository.GetById(id);
            _NotificationRepository.Delete(entity);
        }

        public void RemoveNotification(Notification Notification)
        {
            _NotificationRepository.Delete(Notification);
        }

        public void SaveNotification()
        {
            _unitOfWork.Commit();
        }
    }
}
