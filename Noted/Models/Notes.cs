namespace Noted.Models
{
    public class Notes
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
