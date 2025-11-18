using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioPerfildeRisco.DTOs;

//Deixei o default como admin e 123 para facilitar os testes no Swagger
public class LoginRequest
{
    [Required(ErrorMessage = "Username é obrigatório")]
    [DefaultValue("admin")]
    public string Username { get; set; } = "admin";

    [Required(ErrorMessage = "Password é obrigatório")]
    [DefaultValue("123")]
    public string Password { get; set; } = "123";
}
