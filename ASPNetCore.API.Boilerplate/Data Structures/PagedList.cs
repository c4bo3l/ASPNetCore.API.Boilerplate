using ASPNetCore.API.Boilerplate.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetCore.API.Boilerplate.Data_Structures
{
    public class PagedList<T> : List<T>
    {
        #region Properties
        protected PagingParameter PagingParam { get; private set; }

        protected int TotalItems { get; private set; }

        public int TotalPages { get; set; }

        public bool HasPrevPage {
            get {
                return PagingParam.PageNumber > 1;
            }
        }

        public bool HasNextPage {
            get {
                return PagingParam.PageNumber < TotalPages;
            }
        }
        #endregion

        #region Constructors
        public PagedList(PagingParameter param, int totalItems, int totalPages) {
            PagingParam = param;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
        #endregion

        #region Create
        public static PagedList<T> Create(PagingParameter param, IQueryable<T> items) {
            if (param == null || items == null)
                throw new ArgumentNullException();
            int count = items.Count();
            int totalPages = (int)Math.Ceiling((decimal)count / (decimal)param.PageSize);

            if (param.PageNumber > totalPages)
                param.PageNumber = totalPages;

            items = items.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize);

            PagedList<T> pList = new PagedList<T>(param, count, totalPages);
            pList.AddRange(items);

            return pList;
        }
        #endregion
    }
}
