using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extentions
{
    public static class MappingExtensions
    {
        public static Task<PaginatedListVM<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
            => PaginatedListVM<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
            => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();

        public static Task<TDestination> ProjectToSingle<TSource, TDestination>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>> predicate, IConfigurationProvider configuration) where TDestination: class where TSource : class
        {
            return queryable.Where(predicate).ProjectTo<TDestination>(configuration).FirstOrDefaultAsync();
        }
    }
}
