using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public class Page<TEntity>
    {
        /// <summary>
        /// Total number of items in requested collection
        /// </summary>
        public int TotalNumberOfItems { get; set; }

        /// <summary>
        /// Fetched items collection
        /// </summary>
        public Dictionary<string,TEntity> Items { get; set; }

        /// <summary>
        /// Number of fetched items in current  page
        /// </summary>
        public int NumberOfItems => Items.Count;
        
    }
}
