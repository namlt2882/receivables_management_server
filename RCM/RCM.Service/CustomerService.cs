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
        void CreateCustomer(Customer Customer);
        void EditCustomer(Customer Customer);
        void RemoveCustomer(int id);
        void RemoveCustomer(Customer Customer);
        void SaveCustomer();
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork)
        {
            this._CustomerRepository = CustomerRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateCustomer(Customer Customer)
        {
            _CustomerRepository.Add(Customer);
        }

        public void EditCustomer(Customer Customer)
        {
            var entity = _CustomerRepository.GetById(Customer.Id);
            entity = Customer;
            _CustomerRepository.Update(entity);
        }

        public Customer GetCustomer(int id)
        {
            return _CustomerRepository.GetById(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _CustomerRepository.GetAll();
        }

        public IEnumerable<Customer> GetCustomers(Expression<Func<Customer, bool>> where)
        {
            return _CustomerRepository.GetMany(where);
        }

        public void RemoveCustomer(int id)
        {
            var entity = _CustomerRepository.GetById(id);
            _CustomerRepository.Delete(entity);
        }

        public void RemoveCustomer(Customer Customer)
        {
            _CustomerRepository.Delete(Customer);
        }

        public void SaveCustomer()
        {
            _unitOfWork.Commit();
        }
    }
}
