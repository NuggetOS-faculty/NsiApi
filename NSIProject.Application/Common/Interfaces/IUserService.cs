using NSIProject.Domain.Entities;

namespace NSIProject.Application.Common.Interfaces;

public interface IUserService
{
    Task<ApplicationUser?> FindById(Guid guid);
    Task<ApplicationUser?> FindByUserName(string userName);
}