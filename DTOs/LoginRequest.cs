using System.ComponentModel;

namespace DesafioPerfildeRisco.DTOs;

//Deixei o default como admin e 123 para facilitar os testes no Swagger
public class LoginRequest
{
    [DefaultValue("admin")]
    public string Username { get; set; } = "admin";
    [DefaultValue("123")]
    public string Password { get; set; } = "123";
}
