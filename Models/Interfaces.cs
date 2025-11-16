namespace DesafioPerfilInvestidor.Models;
using DesafioPerfilInvestidor.DTOs;


public interface IInvestimentoServico
{
    Simulacao SimularInvestimento(SimulacaoRequest DadosSimulacao);
}