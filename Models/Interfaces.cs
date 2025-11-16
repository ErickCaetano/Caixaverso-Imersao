namespace DesafioPerfilInvestidor.Models;
using DesafioPerfilInvestidor.DTOs;


public interface IEmprestimoServico
{
    Simulacao SimularInvestimento(SimulacaoRequest DadosSimulacao);
}