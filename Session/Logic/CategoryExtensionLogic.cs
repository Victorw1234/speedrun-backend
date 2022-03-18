using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Logic
{
    public static class CategoryExtensionLogic
    {

        public static CategoryExtension GetCategoryExtension(ApplicationDbContext ctx,int gameId, string urlCategoryExtension)
        {
            return ctx.CategoryExtensions.ToList<CategoryExtension>()
                                        .Where(q => q.UrlTitle == urlCategoryExtension &&
                                                    q.GameId == gameId)
                                        .FirstOrDefault();
        }

    }
}
