using CP3.Application.Dtos;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public void AdicionarBarco(BarcoDto barcoDto)
        {
            var barco = new BarcoEntity
            {
                Nome = barcoDto.Nome,
                Modelo = barcoDto.Modelo,
                Ano = barcoDto.Ano,
                Tamanho = barcoDto.Tamanho
            };
            _repository.Adicionar(barco);
        }

        public void AtualizarBarco(int id, BarcoDto barcoDto)
        {
            var barco = new BarcoEntity
            {
                Id = id,
                Nome = barcoDto.Nome,
                Modelo = barcoDto.Modelo,
                Ano = barcoDto.Ano,
                Tamanho = barcoDto.Tamanho
            };
            _repository.Atualizar(barco);
        }

        public void RemoverBarco(int id)
        {
            _repository.Remover(id);
        }

        public BarcoDto ObterBarcoPorId(int id)
        {
            var barco = _repository.ObterPorId(id);
            if (barco == null) return null;

            return new BarcoDto
            {
                Nome = barco.Nome,
                Modelo = barco.Modelo,
                Ano = barco.Ano,
                Tamanho = barco.Tamanho
            };
        }

        public List<BarcoDto> ObterTodosBarcos()
        {
            var barcos = _repository.ObterTodos();
            return barcos.Select(barco => new BarcoDto
            {
                Nome = barco.Nome,
                Modelo = barco.Modelo,
                Ano = barco.Ano,
                Tamanho = barco.Tamanho
            }).ToList();
        }
    }
}
