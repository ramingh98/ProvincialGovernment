using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Objects
{
    //********************************************************************************************************************
    public class DataTableResponse
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public object data { get; set; }
    }
    //********************************************************************************************************************
}
