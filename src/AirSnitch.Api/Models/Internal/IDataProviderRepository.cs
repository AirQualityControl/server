﻿using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public interface IDataProviderRepository
    {
        /// <summary>
        ///     Returns single data provider by supplied Id
        /// </summary>
        /// <param name="id">Unique identifier of monitoring station</param>
        /// <returns>Instance of airMonitoringStation</returns>
        Task<DataProviderDTO> GetByIdAsync(string id);

        /// <summary>
        ///     Returns a page with a certain number of elements inside.
        /// </summary>
        /// <param name="pageOffset">Offset of the requested page.By default it is 0.
        /// It means that no item will be skipped and result will be return from the beginning of collection</param>
        /// <param name="numberOfItems">Number of items that will be returned inside of the page after successful queery execution.
        /// Default value is 50.Max value that could be returned is 100.</param>
        /// <returns>Returns a page that contains all necessary data.</returns>
        Task<Page<DataProviderDTO>> GetPage(int pageOffset, int numberOfItems = 50);
    }
}