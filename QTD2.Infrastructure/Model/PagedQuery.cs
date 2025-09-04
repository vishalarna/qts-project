using System.ComponentModel.DataAnnotations.Schema;

namespace QTD2.Infrastructure.Model
{
    public class PagedQuery
    {
        private string _orderBy = string.Empty;

        public string OrderBy
        {
            get => _orderBy;
            set
            {
                value ??= string.Empty;
                if (value.StartsWith('-'))
                {
                    _orderBy = value[1..];
                    IsDescending = true;
                }
                else
                {
                    _orderBy = value;
                    IsDescending = false;
                }
            }
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        
        [NotMapped]
        public bool IsDescending { get; private set; }
    }
}
