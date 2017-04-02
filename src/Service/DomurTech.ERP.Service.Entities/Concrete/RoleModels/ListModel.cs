using System;
using System.Collections.Generic;

namespace DomurTech.ERP.Service.Entities.Concrete.RoleModels
{
    public class ListModel
    {
        public List<DetailModel> Items { get; set; }
        public string LanguageCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Searched { get; set; }

    }
}
