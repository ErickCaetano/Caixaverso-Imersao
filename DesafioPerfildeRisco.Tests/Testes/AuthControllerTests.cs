namespace DesafioPerfildeRisco.Tests;

using DesafioPerfildeRisco.Controller;
using DesafioPerfildeRisco.DTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;


    public class AuthControllerTests
    {
        [Fact]
        public void Login_ComCredenciaisIncorretas_RetornaUnauthorized()
        {
            // Dados para teste
            var controller = new AuthController();
            var request = new LoginRequest { Username = "admin", Password = "wrong" };

            // Testar classe
            var result = controller.Login(request);

            // Conferir resultados
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Credenciais inválidas", unauthorized.Value);
        }



        [Fact]
        public void Login_UsuarioOuSenhaVazios_RetornaBadRequest()
        {
            
            // Dados para teste
            var controller = new AuthController();
            var request = new LoginRequest { Username = "", Password = "" };

            // Testar classe
            var result = controller.Login(request);

            // Conferir resultados
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Usuário e senha são obrigatórios", badRequest.Value);
        }

    }


