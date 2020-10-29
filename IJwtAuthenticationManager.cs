using System.Threading.Tasks;

namespace BKSHLF
{
    public interface IJwtAuthenticationManager
    {
        Task<string> Authenticate(string username, string password);
    }
}