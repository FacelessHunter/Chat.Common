using System;

namespace Common.Domain.DTOs
{
    public class ContentDTO<Tdto>
    {
        public Tdto[] Content { get; set; }

        public int TotalCount { get; set; }

        public Uri NextPage { get; set; }

        public Uri PreviousPage { get; set; }
    }
}
