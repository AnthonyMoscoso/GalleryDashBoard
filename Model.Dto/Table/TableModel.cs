using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Table
{
    public class TableModel
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public string Sorted { get; set; }
        public bool IsAsc { get; set; }
        public TableModel()
        {
            Sorted = string.Empty;
        }
        public bool IsEnty()
        {
            return Skip == 0 && Take == 0 && string.IsNullOrEmpty(Sorted);
        }
    }
}
