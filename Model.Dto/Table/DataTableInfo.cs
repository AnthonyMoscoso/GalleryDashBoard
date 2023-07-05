using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Table
{
    public class DataTableInfo<TItem>
    {
        public DataTableInfo()
        {
            Items = new List<TItem>();
        }
        public int TotalItems { get; set; }
        public IEnumerable<TItem>? Items { get; set; }
    }
}
