using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IServiceBase<TEntity, TDto>
    {
        Task<Dto.PageResultDto<TDto>> SearchAsync(Expression<Func<TEntity, bool>> pression = null,
           int skip = 0, int take = 10);

        Task<Dto.PageResultDto<TDto>> SearchAsync<TOrderBy>(Expression<Func<TEntity, bool>> pression = null,
            Expression<Func<TEntity, TOrderBy>> orderBy = null, OrderType orderType = OrderType.Descending,
            int skip = 0, int take = 10);

        Task<Dto.PageResultDto<TDto>> SearchAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int skip = 0, int take = 10);

        Task<IEnumerable<TDto>> FindAllAsync<TOrderBy>(Expression<Func<TEntity, bool>> pression = null,
            Expression<Func<TEntity, TOrderBy>> orderBy = null, OrderType orderType = OrderType.Descending);

        Task<IEnumerable<TDto>> FindAllAsync(Expression<Func<TEntity, bool>> pression = null);

        Task<TDto> FindByIdAsync(params object[] keyValues);

        Task<TDto> CreateAsync(TDto model);

        Task<TDto> UpdateAsync(TDto model);

        Task DeleteAsync(params object[] keyValues);
    }
}
