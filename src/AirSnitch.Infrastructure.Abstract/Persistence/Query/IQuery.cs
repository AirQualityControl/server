using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public interface IQuery
    {
        PageOptions PageOptions { get; set; }

        void AddColumn(QueryColumn queryColumn);
    }

    public readonly struct PageOptions
    {
        private static PageOptions _empty = new PageOptions();
        
        private readonly int _pageNumber;

        private readonly long? _totalNumberOfItems;

        private readonly int _itemsPerPage;

        public PageOptions(int pageNumber, int itemsPerPage = 50)
        {
            _pageNumber = pageNumber;
            _itemsPerPage = itemsPerPage;
            _totalNumberOfItems = null;
        }

        public PageOptions(int pageNumber, long totalNumberOfItems, int itemsPerPage = 50)
        {
            _pageNumber = pageNumber;
            _totalNumberOfItems = totalNumberOfItems;
            _itemsPerPage = itemsPerPage;
        }

        public int PageNumber => _pageNumber;

        public int Items => _itemsPerPage;

        public int ItemsToSkip {
            get
            {
                if (_pageNumber == 1)
                {
                    return 0;
                }

                if (_pageNumber > 1)
                {
                    return (_pageNumber * _itemsPerPage) - _itemsPerPage;
                }

                return 0;
            }
        }

        public int ItemsLimit => _itemsPerPage;

        public long LastPageNumber
        {
            get
            {
                if (_totalNumberOfItems != null)
                {
                    var value = _totalNumberOfItems.Value / _itemsPerPage;

                    var divisionReminder = _totalNumberOfItems.Value % _itemsPerPage;

                    if (divisionReminder > 0)
                    {
                        value += 1;
                    }

                    return value;
                }
                return 0;
            }
        }
    }
}