using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace TestTaskApp.Frontend.Infrastructure.Authentication
{
    public class DummyAuthenticationAttrribute:BasicAuthenticationAttribute
    {
        public static string Username { get; } = "Admin";
        private const string Password = "AdminPwd";
        protected override Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            if (userName != Username || password != Password) return null;

            var identity = new GenericIdentity(userName);
            IPrincipal principal = new GenericPrincipal(identity,null);
            return Task.FromResult(principal);
        }
    }
}