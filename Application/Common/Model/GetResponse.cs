namespace Application.Common.Model
{
    internal class GetResponse<T> where T : class
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount => Items.Count;
    }
}
