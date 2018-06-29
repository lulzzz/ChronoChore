namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBWebServicePaging
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PerPageCount { get; set; }
        public int TotalCount { get; set; }

        public WBWebServicePaging Clone()
        {
            return new WBWebServicePaging()
            {
                PageNumber = this.PageNumber,
                TotalPages = this.TotalPages,
                TotalCount = this.TotalCount,
                PerPageCount = this.PerPageCount
            };
        }
    }
}
