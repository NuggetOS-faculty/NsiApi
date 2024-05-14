using NSIProject.Domain.Entities;

namespace NSIProject.Application.Common.Interfaces;

public interface IUserService
{
    Task<ApplicationUser?> FindById(string id);
    Task<ApplicationUser?> FindByUserName(string userName);
}