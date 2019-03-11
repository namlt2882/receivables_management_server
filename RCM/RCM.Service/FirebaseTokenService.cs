using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Infrastructure;
using RCM.Data.Repositories;
using RCM.Model;
namespace RCM.Service
{

    public interface IFirebaseTokenService
    {
        IEnumerable<FirebaseToken> GetFirebaseTokens();
        IEnumerable<FirebaseToken> GetFirebaseTokens(Expression<Func<FirebaseToken, bool>> where);
        FirebaseToken GetFirebaseToken(int id);
        FirebaseToken CreateFirebaseToken(FirebaseToken FirebaseToken);
        void EditFirebaseToken(FirebaseToken FirebaseToken);
        void RemoveFirebaseToken(int id);
        void RemoveFirebaseToken(Expression<Func<FirebaseToken, bool>> where);
        void SaveFirebaseToken();
    }
    public class FirebaseTokenService : IFirebaseTokenService
    {
        private readonly IFirebaseTokenRepository _FirebaseTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FirebaseTokenService(IFirebaseTokenRepository FirebaseTokenRepository, IUnitOfWork unitOfWork)
        {
            this._FirebaseTokenRepository = FirebaseTokenRepository;
            this._unitOfWork = unitOfWork;
        }

        public FirebaseToken CreateFirebaseToken(FirebaseToken FirebaseToken)
        {
            return _FirebaseTokenRepository.Add(FirebaseToken);
        }

        public void EditFirebaseToken(FirebaseToken FirebaseToken)
        {
            var entity = _FirebaseTokenRepository.GetById(FirebaseToken.Id);
            entity = FirebaseToken;
            _FirebaseTokenRepository.Update(entity);
        }

        public FirebaseToken GetFirebaseToken(int id)
        {
            return _FirebaseTokenRepository.GetById(id);
        }

        public IEnumerable<FirebaseToken> GetFirebaseTokens()
        {
            return _FirebaseTokenRepository.GetAll();
        }

        public IEnumerable<FirebaseToken> GetFirebaseTokens(Expression<Func<FirebaseToken, bool>> where)
        {
            return _FirebaseTokenRepository.GetMany(where);
        }

        public void RemoveFirebaseToken(int id)
        {
            var entity = _FirebaseTokenRepository.GetById(id);
            _FirebaseTokenRepository.Delete(entity);
        }

        public void RemoveFirebaseToken(Expression<Func<FirebaseToken, bool>> where)
        {
            _FirebaseTokenRepository.Delete(where);
        }

        public void SaveFirebaseToken()
        {
            _unitOfWork.Commit();
        }
    }
}
