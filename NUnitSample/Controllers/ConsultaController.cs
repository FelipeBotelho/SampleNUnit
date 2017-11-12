using NUnitSample.Contracts;
using NUnitSample.Service.Interfaces;
using System.Web.Http;

namespace NUnitSample.Controllers
{
    public class ConsultaController : ApiController
    {
        private readonly IConsultaCreditoService _servConsultaCredito;

        public ConsultaController(IConsultaCreditoService servConsultaCredito)
        {
            _servConsultaCredito = servConsultaCredito;
        }

        [HttpGet]
        public StatusConsultaCredito Consultar(string cpf)
        {
            try
            {
                var pendencias = _servConsultaCredito.ConsultarPendenciasPorCPF(cpf);

                if (pendencias == null)
                    return StatusConsultaCredito.ParametroEnvioInvalido;
                else if (pendencias.Count == 0)
                    return StatusConsultaCredito.SemPendencias;
                else
                    return StatusConsultaCredito.Inadimplente;
            }
            catch
            {
                return StatusConsultaCredito.ErroComunicacao;
            }
        }
    }
}