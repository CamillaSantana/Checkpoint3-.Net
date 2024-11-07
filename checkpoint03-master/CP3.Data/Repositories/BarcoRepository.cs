using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Adicionar(BarcoEntity barco)
        {
            _context.Barcos.Add(barco);
            _context.SaveChanges();
        }

        public void Atualizar(BarcoEntity barco)
        {
            var barcoExistente = _context.Barcos.Find(barco.Id);
            if (barcoExistente != null)
            {
                barcoExistente.Nome = barco.Nome;
                barcoExistente.Modelo = barco.Modelo;
                barcoExistente.Ano = barco.Ano;
                barcoExistente.Tamanho = barco.Tamanho;
                _context.SaveChanges();
            }
        }

        public void Remover(int id)
        {
            var barco = _context.Barcos.Find(id);
            if (barco != null)
            {
                _context.Barcos.Remove(barco);
                _context.SaveChanges();
            }
        }

        public BarcoEntity ObterPorId(int id)
        {
            return _context.Barcos.Find(id);
        }

        public List<BarcoEntity> ObterTodos()
        {
            return _context.Barcos.ToList();
        }
    }
}
