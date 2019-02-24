using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;

namespace RCM.Service
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Customer> GetCustomers(Expression<Func<Customer, bool>> where);
        Customer GetCustomer(int id);
        Customer CreateCustomer(Customer customer);
        void EditCustomer(Customer customer);
        void RemoveCustomer(int id);
        void RemoveCustomer(Customer customer);
        void SaveCustomer();
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            this._customerRepository = customerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Customer CreateCustomer(Customer customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.IsDeleted = false;
            return _customerRepository.Add(customer);
        }

        public void EditCustomer(Customer customer)
        {
            var entity = _customerRepository.GetById(customer.Id);
            entity = customer;
            entity.UpdatedDate= DateTime.Now;
            _customerRepository.Update(entity);
        }

        public Customer GetCustomer(int id)
        {
            return _customerRepository.GetById(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetAll();
        }

        public IEnumerable<Customer> GetCustomers(Expression<Func<Customer, bool>> where)
        {
            return _customerRepository.GetMany(where);
        }

        public void RemoveCustomer(int id)
        {
            var entity = _customerRepository.GetById(id);
            _customerRepository.Delete(entity);
        }

        public void RemoveCustomer(Customer customer)
        {
            customer.IsDeleted = true;
            customer.UpdatedDate = DateTime.Now;
            _customerRepository.Delete(customer);
        }

        public void SaveCustomer()
        {
            _unitOfWork.Commit();
        }
    }
}
