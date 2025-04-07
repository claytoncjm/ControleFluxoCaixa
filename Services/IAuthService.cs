using ControleFluxoCaixa.Models;
using System.Threading.Tasks;

namespace ControleFluxoCaixa.Services
{
    // Interface Segregation Principle (ISP) do SOLID
    // Interface específica para o serviço de autenticação
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto model);
        Task<string> LoginAsync(UserLoginDto model);
    }
}
