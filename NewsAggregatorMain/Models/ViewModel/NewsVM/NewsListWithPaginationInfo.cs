using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Models.ViewModel.NewsVM
{
    public class NewsListWithPaginationInfo
    {
        public IEnumerable<NewsGetDTO> News { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
