using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Produtos.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        TEntity Find(int key);
        void Incluir(params TEntity[] obj);
        void Alterar(params TEntity[] obj);
        void Excluir(params TEntity[] obj);
    }
}
