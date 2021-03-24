﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.News
{
   public class RssSource
    {
        public Guid Id { get; set; }
        public string Link { get; set; } // Содержит полный URL адрес до страницы, на которой данный элемент представлен максимально подробно.
        public DateTime DateOfReceiving { get; set; }
        public IEnumerable<Author> Authors{ get; set; }
        public IEnumerable<News> News { get; set; } 
    }
}
