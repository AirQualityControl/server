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

        public int ItemsToSkip => _pageNumber * _itemsPerPage;

        public int ItemsLimit => _itemsPerPage;
    }
}