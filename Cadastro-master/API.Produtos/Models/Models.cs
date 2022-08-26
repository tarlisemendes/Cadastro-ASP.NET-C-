using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API.Produtos.Models
{
    public class Produto 
    {

        public int Id { get; protected set; }
        public string Codigo { get;  set; }
        public string Nome { get;  set; }
        public decimal Valor { get;  set; }
        
        //valor vezes a quantidade
        public int Quantidade { get; set; }
        public decimal SaldoEstoque
        {
            get
            {
                
                return this.SaldoEstoque = this.Quantidade * this.Valor;
                
            }
            set {}
        }

        
        public List<Fornecedor> Fonecedores { get; private set; } = new List<Fornecedor>();
    }
    
    public class Fornecedor
    {

        public int Id { get; protected set; }

        public Produto Produto { get; set; }
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Telefone { get; set; } 
        public string Endereco { get; set; } 
        public string CNPJ { get; set; }

        public int ProdutoId { get; set; }

    }

    public class Usuario
    {
        public int Id { get; protected set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }

        //public List<Produto> Produtos { get; private set; } = new List<Produto>();
        //public List<Fornecedor> Fonecedores { get; private set; } = new List<Fornecedor>();

    }




}
