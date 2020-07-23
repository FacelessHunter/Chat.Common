using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.DTOs.FilterDTOs
{
    public class BaseFilterModel
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public BaseFilterModel()
        {
            Page = 1;
            Limit = 100;
        }
    }
}
