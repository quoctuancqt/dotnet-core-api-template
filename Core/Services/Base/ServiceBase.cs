using Core.Constants;
using Core.Factories;
using Core.Helpers;
using Core.Repository;
using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public abstract class ServiceBase<TEntity, TDto> : IServiceBase<TEntity, TDto>
        where TEntity : class, Domain.IEntity
        where TDto : class, Dto.IDto
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected readonly IRepository<TEntity> _reponsitory;

        public string UserId
        {
            get
            {
                return _unitOfWork.UserId;
            }
        }

        public string UserName
        {
            get
            {
                return _unitOfWork.UserName;
            }
        }

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _reponsitory = unitOfWork.GetPropValue<IRepository<TEntity>>($"{typeof(TEntity).Name}Repository");
        }

        public virtual async Task<TDto> CreateAsync(TDto model)
        {
            var entity = DtoToEntity(model);

            _reponsitory.Add(entity);

            await _unitOfWork.CommitAsync();

            return EntityToDto(entity);
        }

        public virtual async Task<TDto> UpdateAsync(TDto model)
        {
            var entity = DtoToEntity(model);

            _reponsitory.Update(entity);

            await _unitOfWork.CommitAsync();

            return EntityToDto(entity);

        }

        public virtual async Task DeleteAsync(params object[] keyValues)
        {
            var entity = await _reponsitory.FindByAsync(keyValues);

            if (entity == null) throw new Exception("Not found entity object with id: " + keyValues);

            _reponsitory.Remove(entity);

            await _unitOfWork.CommitAsync();
        }

        public virtual async Task<IEnumerable<TDto>> FindAllAsync(Expression<Func<TEntity, bool>> pression = null)
        {
            return EntityToDto(await FindAll(pression).ToListAsync());
        }

        public virtual async Task<IEnumerable<TDto>> FindAllAsync<TOrderBy>(Expression<Func<TEntity, bool>> pression = null,
            Expression<Func<TEntity, TOrderBy>> orderBy = null, OrderType orderType = OrderType.Descending)
        {
            var query = FindAll(pression);

            query = BuildOrderBy(query, orderBy, orderType);

            return EntityToDto(await query.ToListAsync());
        }

        public virtual async Task<TDto> FindByIdAsync(params object[] keyValues)
        {
            return EntityToDto(await _reponsitory.FindByAsync(keyValues));
        }

        public virtual async Task<Dto.PageResultDto<TDto>> SearchAsync(Expression<Func<TEntity, bool>> pression = null,
            int skip = 0, int take = 10)
        {
            var query = FindAll(pression);

            IEnumerable<TEntity> entities = await query.Skip((skip)).Take(take).ToListAsync();

            return new Dto.PageResultDto<TDto>(await query.CountAsync(), UnitHelper.GetTotalPage(await query.CountAsync(), take), EntityToDto(entities));
        }

        public virtual async Task<Dto.PageResultDto<TDto>> SearchAsync<TOrderBy>(Expression<Func<TEntity, bool>> pression = null,
            Expression<Func<TEntity, TOrderBy>> orderBy = null, OrderType orderType = OrderType.Descending,
            int skip = 0, int take = 10)
        {
            var query = FindAll(pression);

            query = BuildOrderBy(query, orderBy, orderType);

            IEnumerable<TEntity> entities = await query.Skip(skip).Take(take).ToListAsync();

            return new Dto.PageResultDto<TDto>(await query.CountAsync(), UnitHelper.GetTotalPage(await query.CountAsync(), take), EntityToDto(entities));
        }

        public async Task<Dto.PageResultDto<TDto>> SearchAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int skip = 0, int take = 10)
        {
            var query = FindAll(filter);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var entities = query.Skip(skip).Take(take);

            return new Dto.PageResultDto<TDto>(await query.CountAsync(), UnitHelper.GetTotalPage(await query.CountAsync(), take), EntityToDto(entities));
        }

        protected IQueryable<TEntity> BuildOrderBy<TOrderBy>(IQueryable<TEntity> query,
          Expression<Func<TEntity, TOrderBy>> orderBy,
          OrderType orderType = OrderType.Descending)
        {
            if (orderBy != null)
            {
                query = (orderType == OrderType.Ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy));
            }

            return query;
        }

        protected IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> pression)
        {
            IQueryable<TEntity> query = _reponsitory.FindAll(pression);

            return query;
        }

        protected TDto EntityToDto(TEntity entity)
        {
            return AutoMapper.Mapper.Map<TDto>(entity);
        }

        protected TEntity DtoToEntity(TDto dto)
        {
            return AutoMapper.Mapper.Map<TEntity>(dto);
        }

        protected TEntity DtoToEntity(TDto dto, TEntity entity)
        {
            return AutoMapper.Mapper.Map(dto, entity);
        }

        protected IEnumerable<TDto> EntityToDto(IEnumerable<TEntity> entities)
        {
            return AutoMapper.Mapper.Map<IEnumerable<TDto>>(entities);
        }

        protected IEnumerable<TEntity> DtoToEntity(IEnumerable<TDto> dto)
        {
            return AutoMapper.Mapper.Map<IEnumerable<TEntity>>(dto);
        }
    }
}
