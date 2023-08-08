using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Objects; 
using System.Dynamic;
using System.Linq.Dynamic;

namespace Common.Functions
{
    public class DataTableBulider<TViewModel> where TViewModel : class
    {
        //********************************************************************************************************************
        //Selected Data As View Model
        private IQueryable<TViewModel> ItemSoruce { get; set; }

        //View Model Columns Name
        private List<string> TableColumns { get; set; }

        //Data Request Config Such as Count, Search Value, and etc
        private DataTableRequest DataTableRequest { get; }


        private List<PropertyInfo> ListPropertyInfo { get; }
        //********************************************************************************************************************
        public DataTableBulider(List<TViewModel> itemSoruce, DataTableRequest dataTableRequest)
        {
            ItemSoruce = itemSoruce.AsQueryable();
            ListPropertyInfo = new List<PropertyInfo>();
            DataTableRequest = dataTableRequest;
        }
        //********************************************************************************************************************
        public DataTableBulider(DataTableRequest dataTableRequest)
        {
            DataTableRequest = dataTableRequest;
        }
        //********************************************************************************************************************
        public DataTableBulider(IEnumerable<TViewModel> itemSoruce, DataTableRequest dataTableRequest)
        {
            ItemSoruce = itemSoruce.AsQueryable();
            ListPropertyInfo = new List<PropertyInfo>();
            DataTableRequest = dataTableRequest;
        }
        //********************************************************************************************************************
        public DataTableBulider(IQueryable<TViewModel> itemSoruce, DataTableRequest dataTableRequest)
        {
            ItemSoruce = itemSoruce;
            ListPropertyInfo = new List<PropertyInfo>();
            DataTableRequest = dataTableRequest;
        }
        //********************************************************************************************************************
        //private string SearchQueryBuilder()
        //{
        //    DataTableRequest.SearchWith = string.IsNullOrEmpty(DataTableRequest.SearchWith) ? string.Empty : DataTableRequest.SearchWith;


        //    var queryString = ;
        //    queryString = queryString.Replace("#", "\"" + DataTableRequest.search.value.ToLower() + "\"");

        //    return queryString;
        //}
        //********************************************************************************************************************
        private IQueryable<TViewModel> ColumnOrder()
        {
            if (DataTableRequest.order == null || string.IsNullOrEmpty(DataTableRequest.order[0].column))
            {
                return ItemSoruce;
            }

            var index = Convert.ToInt32(DataTableRequest.order[0].column);
            return DataTableRequest.order[0].dir == "desc" ?
                ItemSoruce.OrderBy(TableColumns[index] + " descending") :
                ItemSoruce.OrderBy(TableColumns[index]);
        }
        //********************************************************************************************************************
        public void SelectColumn(List<string> columnName)
        {
            TableColumns = columnName.ToList<string>();
            foreach (var name in TableColumns)
            {
                var temp = typeof(TViewModel).GetProperties().Single(c => c.Name == name);
                ListPropertyInfo.Add(temp);
            }
        }
        public DataTableResponse ExecuteQuery()
        {
            var recordTotal = ItemSoruce.Count();

            ItemSoruce = ColumnOrder();

            var result = ItemSoruce.Skip(DataTableRequest.Start).Take(DataTableRequest.Length).ToList();
            var rows = new List<object>();
            foreach (var entity in result)
            {
                dynamic dynamicData = new ExpandoObject();
                var dynamicObject = dynamicData as IDictionary<string, object>;
                foreach (var propertyInfo in ListPropertyInfo)
                {
                    if (propertyInfo.GetValue(entity) != null)
                    {
                        dynamicObject[propertyInfo.Name] = propertyInfo.GetValue(entity).ToString();
                    }
                    else
                    {
                        dynamicObject[propertyInfo.Name] = string.Empty;
                    }
                }
                rows.Add(dynamicObject);
            }

            var responseTableConfig = new DataTableResponse
            {
                draw = DataTableRequest.Draw + 1,
                recordsTotal = recordTotal,
                recordsFiltered = ItemSoruce.Count(),
                data = rows
            };
            return responseTableConfig;
        }
        //********************************************************************************************************************
        public DataTableResponse EmptyResponse()
        {
            var responseTableConfig = new DataTableResponse
            {
                draw = DataTableRequest.Draw + 1,
                recordsTotal = 0,
                recordsFiltered = 0,
                data = new List<object>()
            };
            return responseTableConfig;
        }
        //********************************************************************************************************************
    }
}
