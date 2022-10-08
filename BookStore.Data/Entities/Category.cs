namespace BookStore.Data.Entities
{
    public class Category : BaseEntity <Guid>
    {

        public string CategoryName { get; set; }

        public List<Book> Books { get; set; }



    }
}
