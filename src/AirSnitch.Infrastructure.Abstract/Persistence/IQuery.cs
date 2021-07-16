namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public interface IQuery
    {
        PageOptions PageOptions { get; set; }

        void AddColumn(QueryColumn queryColumn);
    }

    public readonly struct PageOptions
    {
        private readonly int _pageNumber;

        private readonly int _itemsPerPage;
        
        public PageOptions(int pageNumber, int itemsPerPage = 50)
        {
            _pageNumber = pageNumber;
            _itemsPerPage = itemsPerPage;
        }

        public int PageNumber => _pageNumber;

        public int Items => _itemsPerPage;

        public int ItemsToSkip {
            get
            {
                if (_pageNumber > 1)
                {
                    return _pageNumber * _itemsPerPage;
                }

                return 0;
            }
        }

        public int ItemsLimit => _itemsPerPage;
    }
}