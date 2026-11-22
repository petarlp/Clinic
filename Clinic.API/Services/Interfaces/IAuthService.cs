
using System.Threading.Tasks;
using Clinic.API.Models.DTOs;

namespace Clinic.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
}
