using NSIProject.Domain.Entities;

namespace BaseTests.Builders.User
{
    public class ApplicationUserBuilder
    {
        private readonly ApplicationUser _applicationUser;

        public ApplicationUserBuilder()
        {
            _applicationUser = new ApplicationUser();
        }

        public ApplicationUserBuilder WithId(string id)
        {
            _applicationUser.Id = id;
            return this;
        }

        public ApplicationUserBuilder WithUserName(string userName)
        {
            _applicationUser.UserName = userName;
            _applicationUser.NormalizedUserName = userName;
            return this;
        }

        public ApplicationUserBuilder WithEmail(string email)
        {
            _applicationUser.Email = email;
            _applicationUser.NormalizedEmail = email;
            return this;
        }

        public ApplicationUserBuilder WithPasswordHash(string passwordHash)
        {
            _applicationUser.PasswordHash = passwordHash;
            return this;
        }

        public ApplicationUserBuilder WithEmailConfirmed(bool emailConfirmed)
        {
            _applicationUser.EmailConfirmed = emailConfirmed;
            return this;
        }

        public ApplicationUserBuilder WithPhoneNumberConfirmed(bool phoneNumberConfirmed)
        {
            _applicationUser.PhoneNumberConfirmed = phoneNumberConfirmed;
            return this;
        }

        public ApplicationUserBuilder WithSecurityStamp(string securityStamp)
        {
            _applicationUser.SecurityStamp = securityStamp;
            return this;
        }

        public ApplicationUserBuilder WithFirstName(string firstName)
        {
            _applicationUser.FirstName = firstName;
            return this;
        }

        public ApplicationUserBuilder WithLastName(string lastName)
        {
            _applicationUser.LastName = lastName;
            return this;
        }

        public ApplicationUserBuilder WithConcurrencyStamp(string concurrencyStamp)
        {
            _applicationUser.ConcurrencyStamp = concurrencyStamp;
            return this;
        }

        public ApplicationUserBuilder WithAccessFailedCount(int accessFailedCount)
        {
            _applicationUser.AccessFailedCount = accessFailedCount;
            return this;
        }

        public ApplicationUser Build()
        {
            return _applicationUser;
        }
    }
}