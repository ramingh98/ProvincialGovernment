using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Functions
{
   public class SearchQueryBuilder
    {
        string search;
        private List<string> columns { get; set; }
        
        public SearchQueryBuilder(List<string> _cols , string _search)
        {
            columns = _cols;
            search = _search;
        }

        public string BuildQuery()
        {
            string QueryString = "";
            bool FisrtItem = true;

            foreach (string col in columns)
            {
                if (!FisrtItem)
                {
                    QueryString += " || ";
                }
                QueryString += col + ".ToString().ToLower().Contains(?) ";

                FisrtItem = false;
            }
            QueryString = QueryString.Replace("?", "\"" + search.ToLower() + "\"");
            return QueryString;
        }

    }
}
