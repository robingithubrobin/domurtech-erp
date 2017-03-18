using System.Collections.Generic;

namespace DomurTech.ERP.Business.Entities.ComplexTypes
{
    public class Paging
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<KeyValuePair<int, string>> PageSizeList { get; set; }
    }
}
