using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewsAggregatorMain.Models.ViewModel.NewsVM
{
    public class CreateNewsViewModel 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }

        public Guid? RssSourseId { get; set; }

        public SelectList Sources { get; set; }
    }
}
