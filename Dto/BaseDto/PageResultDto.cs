using System.Collections.Generic;

namespace Dto
{
    public class PageResultDto<TDto>
    {
        public PageResultDto(int totalRecord, IEnumerable<TDto> items)
        {
            TotalRecord = totalRecord;
            Items = items;
        }

        public PageResultDto(int totalRecord, int totalPage, IEnumerable<TDto> items)
        {
            TotalRecord = totalRecord;
            ToTalPage = totalPage;
            Items = items;
        }

        public int TotalRecord { get; set; }

        public int ToTalPage { get; set; }

        public IEnumerable<TDto> Items { get; set; }
    }
}
