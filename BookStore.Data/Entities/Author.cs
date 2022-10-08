
namespace BookStore.Data.Entities
{
    public class Author:BaseEntity<int>
    {
        public string AuthorName { get; set; }

        public string AuthorBirthDate { get; set; }    
        
    }
}
