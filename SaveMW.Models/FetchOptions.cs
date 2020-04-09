using System.Web.UI.WebControls;

namespace SaveMW.Models
{
    public class FetchOptions
    {
        public int Start { get; set; }

        public int Count { get; set; }

        public string SortExpression { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
