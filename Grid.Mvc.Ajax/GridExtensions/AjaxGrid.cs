﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Grid.Mvc.Ajax.Helpers;
using GridMvc;

namespace Grid.Mvc.Ajax.GridExtensions
{
    public class AjaxGrid<T> : Grid<T>, IAjaxGrid where T: class
    {
        public IAjaxGridPager AjaxGridSettings { get { return Pager as IAjaxGridPager; } }
        public bool HasItems { get { return Pager.CurrentPage <= AjaxGridSettings.Pages; } }

        public AjaxGrid(IQueryable<T> items, int page, bool renderOnlyRows, int pagePartitionSize=0)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page };
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            AjaxGridSettings.PagePartitionSize = pagePartitionSize;
        }

        public AjaxGrid(IEnumerable<T> items, int page,int count, int pageSize,bool renderOnlyRows, int pagePartitionSize = 0)
            : base(items)
        {
            Pager = new AjaxGridPager(this,count) { CurrentPage = page,ItemCount = count};
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            AjaxGridSettings.PagePartitionSize = pagePartitionSize;
            Pager.PageSize = pageSize;
            AjaxGridSettings.ItemCount = count;
            int pages = count / Pager.PageSize;
            if (count % Pager.PageSize > 0)
                pages++;
            AjaxGridSettings.Pages = pages;
        }

        public string ToJson(string gridPartialViewName, Controller controller)
        {
            var htmlHelper = new KlaHtmlHelpers();
            return htmlHelper.RenderPartialViewToString(gridPartialViewName, this, controller);
        }

        public string ToJson(string gridPartialViewName, Object model, Controller controller)
        {
            var htmlHelper = new KlaHtmlHelpers();
            return htmlHelper.RenderPartialViewToString(gridPartialViewName, model, controller);
        }
    }
}