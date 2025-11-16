namespace DesafioPerfilInvestidor.DTOs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class SimulacaoRequest
{
    //Request da Simulação

    [Required(ErrorMessage = "IdCliente é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "IdCliente deve ser maior que zero")]
    public int IdCliente { get; set; }


    [Required(ErrorMessage = "ValorInvestido é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "ValorInvestido deve ser positivo")]
    [DefaultValue(10000.00)]
    public decimal ValorInvestido { get; set; } = 10000.00m;


    [Required(ErrorMessage = "PrazoMeses é obrigatório")]
    [Range(1, 420, ErrorMessage = "PrazoMeses deve estar entre 1 e 420.")]
    [DefaultValue(12)]
    public int PrazoMeses { get; set; } = 12;

    [Required(ErrorMessage = "TipoProduto é obrigatório")]
    [RegularExpression("^(CDB|LCI|LCA|FUNDO|AÇÕES)$", ErrorMessage = "TipoProduto deve ser CDB, LCI, LCA, AÇÕES ou FUNDO")]
    public string TipoProduto { get; set; } = "CDB";

}