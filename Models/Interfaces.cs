namespace DesafioPerfilInvestidor.Models;
using DesafioPerfilInvestidor.DTOs;


public interface IInvestimentoServico
{
    Task<Simulacao> SimularInvestimento(SimulacaoRequest DadosSimulacao);
}