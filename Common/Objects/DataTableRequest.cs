using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Objects
{
    //********************************************************************************************************************
    public class DataTableRequest
    {
        public List<string> RequestColumns { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public int Draw { get; set; }
        public string SearchWith { get; set; }
        public string Filters { get; set; }
    }
    //********************************************************************************************************************
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
    //********************************************************************************************************************
    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
    //********************************************************************************************************************
    public class Order
    {
        public string column { get; set; }
        public string dir { get; set; }
    }
    //********************************************************************************************************************
}
