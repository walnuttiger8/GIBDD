using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Services
{
    public class UserNotFoundException : Exception { }
    public class AuthenticationFail : Exception { }
    public class TooManyAuthorizationAttempts : Exception { }

    public class AuthorizationService : IDisposable
    {
        private const int _AUTHORIZATION_ATTEMPTS_THRESHOLD_MINUTES = 1;
        private const int _MAX_AUTHORIZATION_ATTEMPTS = 3;
        private readonly GIBDDEntities _db;

        public AuthorizationService()
        {
            _db = new GIBDDEntities();
        }
        public AuthorizationService(GIBDDEntities db)
        {
            _db = db;
        }
        public Users Auth(string login, string password)
        {
            var authorizationAttempts = GetAuthorizationAttempts(login);
            if (authorizationAttempts.Count >= _MAX_AUTHORIZATION_ATTEMPTS)
            {
                throw new TooManyAuthorizationAttempts();
            }

            RegisterAuthorizationAttempt(login);

            var user = Identify(login);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (Authenticate(user, password))
            {
                return user;
            }
            throw new AuthenticationFail();

        }

        private Users Identify(string login)
        {
            var user = _db.Users.Where(x => x.Login == login).FirstOrDefault();
            return user;
        }

        private bool Authenticate(Users user, string password)
        {
            return user.Password == password;
        }

        private string GetPersonIdentity()
        {
            return Environment.MachineName.ToString();
        }

        private void RegisterAuthorizationAttempt(string login)
        {
            _db.AuthorizationAttempts.Add(
                new AuthorizationAttempts()
                {
                    Login = login,
                    PersonIdentity = GetPersonIdentity(),
                    AttemptDate = DateTime.Now
                }
            );
            _db.SaveChanges();
        }

        private List<AuthorizationAttempts> GetAuthorizationAttempts(string login)
        {
            var sinceDate = DateTime.Now;
            sinceDate = sinceDate.AddMinutes(-_AUTHORIZATION_ATTEMPTS_THRESHOLD_MINUTES);

            return _db.AuthorizationAttempts.Where(x =>x.Login == login).Where(x => x.AttemptDate > sinceDate).ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
