using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        /// <summary>
        /// Métodos get que retornará uma lista de lotes por eventoId
        /// </summary>
        /// <param name="eventoId"></param> Código chave da tabela evento
        /// <returns>Array de lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        
        /// <summary>
        /// 
        /// </summary>
        /// Métodos get que retornará apenas 1 lote
        /// <param name="eventoId"> Código chave da tabela evento</param>
        /// <param name="id">Código chave da tabela lote</param> 
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);

    }
}