using Base.Domain.Retornos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.Repository.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<Retorno> GetAll();
        Task<Retorno> GetById(int id);
        Task<Retorno> Cadastrar(TEntity obj);
        Retorno Atualizar(TEntity obj);
        Task<Retorno> Excluir(int id);

        public int SaveChanges();
    }
}
