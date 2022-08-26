using API.Produtos.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Produtos.DAL
{
    public class Data : IData
    {
        private readonly Context _contexto;
        private readonly IRepository<Produto> _produtoRepository;
        private readonly IRepository<Fornecedor> _fornecedorRepository;
        private readonly IRepository<Usuario> _usuarioRepository;


        public Data(Context contexto, IRepository<Produto> produtoRepository, 
            IRepository<Fornecedor> fornecedorRepository, IRepository<Usuario> usuarioRepository)
        {
            this._contexto = contexto;
            this._produtoRepository = produtoRepository;
            this._fornecedorRepository = fornecedorRepository;
            this._usuarioRepository = usuarioRepository;
        }

        
        public void InicializaDB()
        {
            _contexto.Database.EnsureCreated();

            //List<Usuario> usuarios = GetUsuarios();

            //_usuarioRepository.IncluirLista(usuarios);

            List<Produto> produtos = GetProdutos();

            _produtoRepository.IncluirLista(produtos);

            List<Fornecedor> fornecedores = GetFornecedores();

            _fornecedorRepository.IncluirLista(fornecedores);
        }

        private static List<Produto> GetProdutos()
        {
            var json = File.ReadAllText("produtos.json");
            var produtos = JsonConvert.DeserializeObject<List<Produto>>(json);
            return produtos;
        }

        private static List<Fornecedor> GetFornecedores()
        {
            var json = File.ReadAllText("fornecedor-produto.json");
            var fornecedores = JsonConvert.DeserializeObject<List<Fornecedor>>(json);
            return fornecedores;
        }

        private static List<Usuario> GetUsuarios()
        {
            var json = File.ReadAllText("usuario.json");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);
            return usuarios;
        }
    }


}
