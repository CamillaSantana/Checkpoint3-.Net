using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationContext(options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarcoAoBanco()
        {
            var barco = new BarcoEntity { Nome = "Barco Teste", Modelo = "Modelo Teste", Ano = 2023, Tamanho = 25.5 };

            _barcoRepository.Adicionar(barco);
            var barcoNoBanco = _context.Barcos.FirstOrDefault(b => b.Nome == "Barco Teste");

            Assert.NotNull(barcoNoBanco);
            Assert.Equal("Barco Teste", barcoNoBanco.Nome);
        }

        [Fact]
        public void Atualizar_DeveAtualizarDadosDoBarco()
        {
            var barco = new BarcoEntity { Nome = "Barco Antigo", Modelo = "Modelo Antigo", Ano = 2020, Tamanho = 20.0 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            barco.Nome = "Barco Atualizado";
            barco.Modelo = "Modelo Atualizado";
            _barcoRepository.Atualizar(barco);

            var barcoAtualizado = _context.Barcos.Find(barco.Id);
            Assert.Equal("Barco Atualizado", barcoAtualizado.Nome);
            Assert.Equal("Modelo Atualizado", barcoAtualizado.Modelo);
        }

        [Fact]
        public void Remover_DeveRemoverBarcoDoBanco()
        {
            var barco = new BarcoEntity { Nome = "Barco a Ser Removido", Modelo = "Modelo Removido", Ano = 2022, Tamanho = 30.0 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            _barcoRepository.Remover(barco.Id);

            var barcoRemovido = _context.Barcos.Find(barco.Id);
            Assert.Null(barcoRemovido);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoQuandoExistir()
        {
            var barco = new BarcoEntity { Nome = "Barco Existente", Modelo = "Modelo Existente", Ano = 2019, Tamanho = 15.0 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            var barcoObtido = _barcoRepository.ObterPorId(barco.Id);
            Assert.NotNull(barcoObtido);
            Assert.Equal("Barco Existente", barcoObtido.Nome);
        }

        [Fact]
        public void ObterTodos_DeveRetornarTodosOsBarcos()
        {
            _context.Barcos.Add(new BarcoEntity { Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2018, Tamanho = 10.0 });
            _context.Barcos.Add(new BarcoEntity { Nome = "Barco 2", Modelo = "Modelo 2", Ano = 2021, Tamanho = 20.0 });
            _context.SaveChanges();

            var barcos = _barcoRepository.ObterTodos();
            Assert.Equal(2, barcos.Count);
        }
    }
}
