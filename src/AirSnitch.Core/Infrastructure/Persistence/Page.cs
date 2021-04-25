using System;
using System.Collections.Generic;
using System.Text;

namespace AirSnitch.Core.Infrastructure.Persistence
{
    /// <summary>
    /// ADT that represents a paginated result fetched from DB
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Page<TEntity>
    {
        /// <summary>
        /// Total number of items in requested collection
        /// </summary>
        public int TotalNumberOfItems { get; set; }

        /// <summary>
        /// Fetched items collection
        /// </summary>
        public IReadOnlyCollection<TEntity> Items { get; set; }

        /// <summary>
        /// Number of fetched items in current  page
        /// </summary>
        public int NumberOfItems => Items.Count;
    }
}
