using Auth.Application.Commands;
using Auth.Application.JWT;
using Auth.Infrastructure.Repository;


namespace Auth.Application.Handlers
{
    public class LoginHandler
    {
        private readonly UserRepository _repo;
        private readonly JwtService _jwt;

        public LoginHandler(UserRepository repo, JwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public async Task<string> Handle(LoginCommand cmd)
        {
            var user = await _repo.GetByEmailAsync(cmd.Email)
                ?? throw new Exception("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(cmd.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _jwt.GenerateToken(user);
        }
    }
}