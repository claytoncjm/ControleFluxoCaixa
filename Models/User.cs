using System.ComponentModel.DataAnnotations;

namespace ControleFluxoCaixa.Models
{
    // Aplicando o princípio SRP (Single Responsibility Principle) do SOLID
    // Esta classe tem uma única responsabilidade: representar um usuário
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O usuário deve ter entre 3 e 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string PasswordHash { get; set; }
    }

    // DTO Pattern - Separa a representação de dados da entidade
    public class UserLoginDto
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }

    public class UserRegisterDto
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
