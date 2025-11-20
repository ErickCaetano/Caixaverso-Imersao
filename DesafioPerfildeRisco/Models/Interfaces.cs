namespace DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.DTOs;


public interface IInvestimentoServico
{
    Task<Simulacao> SimularInvestimento(SimulacaoRequest DadosSimulacao);
}