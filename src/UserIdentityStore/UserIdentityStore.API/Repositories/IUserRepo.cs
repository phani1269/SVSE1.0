using UserIdentityStore.API.DataLayer.Models;

namespace UserIdentityStore.API.Repositories
{
    public interface IUserRepo
    {
        Task<AuthResult> UserLogin(LoginModel model);
        Task<ResponseModel> UserRegister(RegisterModel model);
        Task<ResponseModel> AdminRegister(RegisterModel model);

    }
}
