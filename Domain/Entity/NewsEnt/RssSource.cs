using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.NewsEnt
{
   public class RssSource : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; } // Содержит полный URL адрес до страницы, на которой данный элемент представлен максимально подробно.
        public DateTime DateOfReceiving { get; set; } = DateTime.Now;
        public IEnumerable<Author> Authors{ get; set; }
        public IEnumerable<News> News { get; set; } 
    }
}
