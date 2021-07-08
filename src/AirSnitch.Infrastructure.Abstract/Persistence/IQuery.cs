namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public interface IQuery
    {
        void AddColumn(QueryColumn queryColumn);

        void AddPageOptions(PageOptions pageOptions);
    }

    public readonly struct PageOptions
    {
        public PageOptions(int pageNumber, int itemsPerPage = 50)
        {
            PageNumber = pageNumber;
            ItemsPerPage = itemsPerPage;
        }
        public int PageNumber { get; }
        public int ItemsPerPage { get;}
    }
}