using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Infra.Helpers.Paginacao
{
    public static class Paginacao
    {
        /// <summary>
        /// Classe genérica de paginação
        /// </summary>
        /// <typeparam name="T">Classe - Model</typeparam>
        /// <typeparam name="N">Classe - DTO</typeparam>
        /// <param name="entities">Objeto DBSet - contexto</param>
        /// <param name="QueryDTO">Query pré-montada os filtros e tipada com o DTO</param>
        /// <param name="QtdPorPagina">Quantidade de registros por página</param>
        /// <param name="PagAtual">Número da página Atual</param>
        /// <returns></returns>
        public static List<retornoPaginacao<N>> GetPage<T, N>(this DbSet<T> entities, IQueryable<N> QueryDTO, int QtdPorPagina, int PagAtual)
        where T : class
        where N : class
        {
            //List<Object> resultado = new List<object>();
            var dadosret = new retornoPaginacao<N>();
            var listaRet = new List<retornoPaginacao<N>>(); // Isso é um armengo, se defazer quebra a aplicacao inteira
            //r resultado = dadosret.Lista();
            dadosret.Lista.Clear();
            try
            {
                // Valida parametros em branco
                QtdPorPagina = QtdPorPagina > 0 ? QtdPorPagina : 15;
                PagAtual = PagAtual > 0 ? PagAtual : 1;
                var QuantReg = 0;

                var lista = QueryDTO;
                QuantReg = QueryDTO.Count();

                if (QuantReg > 0)
                {
                    // Arredonda para cima quando a divisão não for exata
                    var QuantPagina = (decimal)QuantReg / QtdPorPagina;
                    QuantPagina = Math.Ceiling(QuantPagina);

                    // Valida a quantidade máxima de Paginas
                    PagAtual = PagAtual > QuantPagina ? (int)QuantPagina : PagAtual;

                    lista = lista.Skip((PagAtual - 1) * QtdPorPagina)
                                 .Take(QtdPorPagina);
                }
                dadosret.Total = QuantReg;
                dadosret.Lista = lista.ToList();
                listaRet.Add(dadosret);
                //resultado.Add(new { total = QuantReg, lista = lista.ToList() });

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
                // resultado.Add(new { total = 1, lista = new List<N>() });
            }

            return listaRet;
        }

    }


    public class retornoPaginacao<N>
    {
        public int Total { get; set; }
        public List<N> Lista { get; set; }

        public retornoPaginacao()
        {
            Lista = new List<N>();
        }
    }
}
