using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;
using RCM.Helper;

namespace RCM.Service
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        IEnumerable<Contact> GetContacts(Expression<Func<Contact, bool>> where);
        Contact GetContact(int id);
        Contact CreateContact(Contact contact);
        void EditContact(Contact contact);
        void RemoveContact(int id);
        void RemoveContact(Contact contact);
        void SaveContact();
        void ChangePhoneNumberToDefault();
    }

    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            this._contactRepository = contactRepository;
            this._unitOfWork = unitOfWork;
        }

        public void ChangePhoneNumberToDefault()
        {
            GetContacts().ToList().ForEach((x) =>
                {
                    x.Phone = Constant.DEFAULT_PHONE_NUMBER;
                    EditContact(x);
                    SaveContact();
                }
            );
        }

        public Contact CreateContact(Contact contact)
        {
            contact.CreatedDate = DateTime.Now;
            contact.IsDeleted = false;
            return _contactRepository.Add(contact);
        }

        public void EditContact(Contact contact)
        {
            var entity = _contactRepository.GetById(contact.Id);
            entity = contact;
            entity.UpdatedDate = DateTime.Now;
            _contactRepository.Update(entity);
        }

        public Contact GetContact(int id)
        {
            return _contactRepository.GetById(id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _contactRepository.GetAll();
        }

        public IEnumerable<Contact> GetContacts(Expression<Func<Contact, bool>> where)
        {
            return _contactRepository.GetMany(where);
        }

        public void RemoveContact(int id)
        {
            var entity = _contactRepository.GetById(id);
            _contactRepository.Delete(entity);
        }

        public void RemoveContact(Contact contact)
        {
            contact.IsDeleted = true;
            contact.UpdatedDate = DateTime.Now;
            _contactRepository.Delete(contact);
        }

        public void SaveContact()
        {
            _unitOfWork.Commit();
        }
    }
}
