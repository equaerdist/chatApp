namespace WebApplication5.Services.PagedList
{
    public class PagedList<T>
    {
      
        public IEnumerable<T> ListItems {get;}
        public int RequestedPageSize {get; }
        public int PageSize {get;}
        private PagedList(List<T> items, int requestedPageSize, int pageSize)
        {
            ListItems = items;
            RequestedPageSize = requestedPageSize;
            PageSize = pageSize;
        }
    }
}