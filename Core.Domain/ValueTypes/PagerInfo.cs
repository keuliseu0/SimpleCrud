namespace Core.Domain.ValueTypes
{
	public class PagerInfo
	{
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
	}
}
