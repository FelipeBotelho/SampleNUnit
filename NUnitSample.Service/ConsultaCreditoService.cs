using NUnitSample.Service.Interfaces;
using System;
using System.Collections.Generic;
using NUnitSample.Contracts;

namespace NUnitSample.Service
{
    public class ConsultaCreditoService : IConsultaCreditoService
    {
        public IList<Pendencia> ConsultarPendenciasPorCPF(string cpf)
        {
            //Logica para validar se o CPF possui alguma pendencia...
            //Vou fazer algo bobo, para retornar randomicamente.
            Random rnd = new Random();
            int number = rnd.Next(1, 11);
            if (cpf.Length < 11)
                return null;

            else if (number % 2 == 0)
                return new List<Pendencia>();

            return new List<Pendencia>()
            {
                new Pendencia()
                {
                    CPF = cpf,
                    DataPendencia = DateTime.Now,
                    DescricaoPendencia = "Pendencia",
                    NomePessoa = "Pessoa",
                    NomeReclamante = "Reclamante",
                    VlPendencia = 201.10
                }
            };
        }
    }
}
