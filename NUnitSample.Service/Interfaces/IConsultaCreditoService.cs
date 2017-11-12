using NUnitSample.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitSample.Service.Interfaces
{
    public interface IConsultaCreditoService
    {
        IList<Pendencia> ConsultarPendenciasPorCPF(string cpf);
    }
}
