using Common.Collection;
using Microsoft.EntityFrameworkCore;

namespace Common.Paging
{
    #region Devolvemos el objeto con la collección, paginas y el total de registros 
    public static class PagingExtension
    {
        
        public static async Task<DataCollection<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int take)
        {
            var originalPages = page;
            page--;
            if (page > 0)
                page = page * take;
            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Pages = originalPages
            };
            if(result.Total>0)
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total)/take));
            return result;
        }
    }
    #endregion
}
