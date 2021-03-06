﻿using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity.NewsEnt
{
    public class Category : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<News> News { get; set; }
    }
}
