using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Task_SuggestionTypes : Common.Entity
    {
        public string Name { get; set; }

        public Task_SuggestionTypes()
        {
        }

        public Task_SuggestionTypes(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}