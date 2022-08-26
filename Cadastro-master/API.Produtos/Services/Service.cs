using API.Produtos.DAL;
using API.Produtos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Produtos.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {

        private readonly Repository<Produto> _repository;

        public Service(Repository<Produto> repository)
        {
            _repository = repository;
        }

        public IQueryable<TEntity> All => throw new NotImplementedException();

        public void Alterar(params TEntity[] obj)
        {
            throw new NotImplementedException();
        }

        public void Excluir(params TEntity[] obj)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(int key)
        {
            throw new NotImplementedException();
        }

        public void Incluir(params TEntity[] obj)
        {
            //_repository.Incluir(obj);
        }
    }
}
