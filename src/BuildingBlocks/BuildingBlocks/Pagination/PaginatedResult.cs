namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) where TEntity : class
    {
        public int pageIndex { get; set; } = pageIndex;
        public int pageSize { get; set; } = pageSize;
        public long count { get; set; } = count;
        public IEnumerable<TEntity> Data { get; } = data;
    }
}
