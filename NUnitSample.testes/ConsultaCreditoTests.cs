using System;
using NUnit.Framework;
using Moq;
using NUnitSample.Service.Interfaces;
using NUnitSample.Contracts;
using System.Collections.Generic;
using NUnitSample.Controllers;

namespace NUnitSample.testes
{
    [TestFixture]
    public class ConsultaCreditoTests
    {
        private Mock<IConsultaCreditoService> mock;

        private const string CPF_INVALIDO = "123A";
        private const string CPF_ERRO_COMUNICACAO = "76217486300";
        private const string CPF_SEM_PENDENCIAS = "60487583752";
        private const string CPF_INADIMPLENTE = "82226651209";


        [SetUp]
        public void InicializarMockObject()
        {
            mock = new Mock<IConsultaCreditoService>();

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_INVALIDO))
                .Returns(() => null);

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_ERRO_COMUNICACAO))
                .Throws(new Exception("Testando erro de comunicação..."));

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_SEM_PENDENCIAS))
                .Returns(() => new List<Pendencia>());

            List<Pendencia> pendencias = new List<Pendencia>();
            pendencias.Add(new Pendencia()
            {
                CPF = CPF_INADIMPLENTE,
                NomePessoa = "João da Silva",
                NomeReclamante = "ACME Comercial LTDA",
                DescricaoPendencia = "Cartão de Crédito",
                DataPendencia = new DateTime(2015, 02, 14),
                VlPendencia = 600.47
            });

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_INADIMPLENTE)).Returns(() => pendencias);
        }

        private StatusConsultaCredito ObterStatusAnaliseCredito(string cpf)
        {
            ConsultaController consulta = new ConsultaController(mock.Object);
            return consulta.Consultar(cpf);
        }

        [Test]
        public void TestarParametroInvalido()
        {
            StatusConsultaCredito status = ObterStatusAnaliseCredito(CPF_INVALIDO);
            Assert.AreEqual(StatusConsultaCredito.ParametroEnvioInvalido, status);
        }

        [Test]
        public void TestarErroComunicacao()
        {
            StatusConsultaCredito status = ObterStatusAnaliseCredito(CPF_ERRO_COMUNICACAO);
            Assert.AreEqual(StatusConsultaCredito.ErroComunicacao, status);
        }

        [Test]
        public void TestarCPFSemPendencias()
        {
            StatusConsultaCredito status = ObterStatusAnaliseCredito(CPF_SEM_PENDENCIAS);
            Assert.AreEqual(StatusConsultaCredito.SemPendencias, status);
        }

        [Test]
        public void TestarCPFInadimplente()
        {
            StatusConsultaCredito status = ObterStatusAnaliseCredito(CPF_INADIMPLENTE);
            Assert.AreEqual(StatusConsultaCredito.Inadimplente, status);
        }
    }
}
