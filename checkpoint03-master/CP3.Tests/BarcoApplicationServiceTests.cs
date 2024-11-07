using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void Adicionar_DeveChamarMetodoAdicionarDoRepositorio()
        {
            var barco = new BarcoEntity { Nome = "Barco Teste", Modelo = "Modelo Teste", Ano = 2023, Tamanho = 25.5 };

            _barcoService.Adicionar(barco);

            _repositoryMock.Verify(r => r.Adicionar(barco), Times.Once);
        }

        [Fact]
        public void Atualizar_DeveChamarMetodoAtualizarDoRepositorio()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Atualizado", Modelo = "Modelo Atualizado", Ano = 2023, Tamanho = 30.0 };

            _barcoService.Atualizar(barco);

            _repositoryMock.Verify(r => r.Atualizar(barco), Times.Once);
        }

        [Fact]
        public void Remover_DeveChamarMetodoRemoverDoRepositorio()
        {
            int barcoId = 1;

            _barcoService.Remover(barcoId);

            _repositoryMock.Verify(r => r.Remover(barcoId), Times.Once);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoQuandoExistir()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Existente", Modelo = "Modelo Existente", Ano = 2019, Tamanho = 15.0 };
            _repositoryMock.Setup(r => r.ObterPorId(barco.Id)).Returns(barco);

            var result = _barcoService.ObterPorId(barco.Id);

            Assert.NotNull(result);
            Assert.Equal("Barco Existente", result.Nome);
        }

        [Fact]
        public void ObterTodos_DeveRetornarListaDeBarcos()
        {
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2018, Tamanho = 10.0 },
                new BarcoEntity { Id = 2, Nome = "Barco 2", Modelo = "Modelo 2", Ano = 2021, Tamanho = 20.0 }
            };
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(barcos);

            var result = _barcoService.ObterTodos();

            Assert.Equal(2, result.Count);
            Assert.Equal("Barco 1", result[0].Nome);
            Assert.Equal("Barco 2", result[1].Nome);
        }
    }
}
