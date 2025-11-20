namespace DesafioPerfildeRisco.Testes;
using Xunit;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.Services;


    public class ProdutoTests
    {
        [Fact]
        public void TestarSe_ConstrutorEValido()
        {

            // Dados para teste
            int IdProduto = 10;
            string Nome = "CDB Caixa 2026";
            string Tipo  = "CDB";
            decimal Rentabilidade = 0.15m;
            string Risco = "Baixo";

            // Testar classe
            var produto = new Produto(IdProduto, Nome, Tipo, Rentabilidade, Risco);

            // Conferir resultados
            Assert.Equal(IdProduto, produto.IdProduto);
            Assert.Equal(Nome, produto.Nome);
            Assert.Equal(Tipo, produto.Tipo);
            Assert.Equal(Rentabilidade, produto.Rentabilidade);
            Assert.Equal(Risco, produto.Risco);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void TestarSe_IdProdutoMaiorQueUm_RetornaExcecao(int idProduto)
        {
            // Dados para teste
           // int IdProduto = 10;
            string Nome = "CDB Caixa 2026";
            string Tipo  = "CDB";
            decimal Rentabilidade = -0.15m;
            string Risco = "Baixo";

            // Testar e conferir 
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Produto(idProduto, Nome, Tipo, Rentabilidade, Risco));
            Assert.Contains("O ID do produto deve ser maior que 1.", ex.Message);
        }   


        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void TestarSe_NomeEValido_RetornaExcecao(string nome)
        {
            // Dados para teste
            int IdProduto = 10;
           // string Nome = "CDB Caixa 2026";
            string Tipo  = "CDB";
            decimal Rentabilidade = 0.15m;
            string Risco = "Baixo";

            // Testar e conferir 
            var ex = Assert.Throws<ArgumentException>(() =>
            new Produto(IdProduto, nome, Tipo, Rentabilidade, Risco));
            Assert.Contains("Nome não pode ser vazio", ex.Message);
            
        }


        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void TestarSe_TipoDoProdutoEValido_RetornaExcecao(string tipo)
        {
            // Dados para teste
            int IdProduto = 10;
            string Nome = "CDB Caixa 2026";
            //string Tipo  = "CDB";
            decimal Rentabilidade = 0.15m;
            string Risco = "Baixo";

            // Testar e conferir 
            var ex = Assert.Throws<ArgumentException>(() =>
            new Produto(IdProduto, Nome, tipo, Rentabilidade, Risco));
            Assert.Contains("Tipo de produto não pode ser vazio", ex.Message);
            
        }

        [Fact]
        public void TestarSe_RentabilidadeNegativa_RetornaExcecao()
        {
            // Dados para teste
            int IdProduto = 10;
            string Nome = "CDB Caixa 2026";
            string Tipo  = "CDB";
            decimal Rentabilidade = -0.15m;
            string Risco = "Baixo";

            // Testar e conferir 
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Produto(IdProduto, Nome, Tipo, Rentabilidade, Risco));
            Assert.Contains("A rentabilidade não pode ser negativa.", ex.Message);
            
        }

        [Theory]        
        [InlineData("")]
        [InlineData("  ")]        
        public void TestarSe_RiscoEValido_RetornaExcecao(string risco)
        {
            // Dados para teste
            int IdProduto = 10;
            string Nome = "CDB Caixa 2026";
            string Tipo  = "CDB";
            decimal Rentabilidade = 0.15m;
            // string Risco = "Baixo";

            // Testar e conferir 
            var ex = Assert.Throws<ArgumentException>(() =>
            new Produto(IdProduto, Nome, Tipo, Rentabilidade, risco));
            Assert.Contains("O tipo de risco não pode ser vazio", ex.Message);
            
        }

        [Theory]
        [InlineData("Abacaxi")]
        [InlineData("Muito Alto")]
        [InlineData("Perigoso")]        
        public void TestarSe_RiscoEscolhidoValido_RetornaExcecao(string risco)
        {
            // Dados para teste
            int IdProduto = 10;
            string Nome = "CDB Caixa 2026";
            string Tipo  = "CDB";
            decimal Rentabilidade = 0.15m;
            // string Risco = "Baixo";

            // Testar e conferir 
            var ex = Assert.Throws<ArgumentException>(() =>
            new Produto(IdProduto, Nome, Tipo, Rentabilidade, risco));
            Assert.Contains("O tipo de risco precisa ser Baixo, Médio ou Alto.", ex.Message);
            
        }


    }
