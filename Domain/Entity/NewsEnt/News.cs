﻿using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entity.NewsEnt
{
    [Table("News")]
    public class News : IBaseEntity 
    { 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string HeadImgUrl { get; set; }
        public string Body { get; set; }

        public float Rating { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public Guid? RssSourceId { get; set; }
        public RssSource RssSource { get; set; }

        public Guid? CategoryId { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
