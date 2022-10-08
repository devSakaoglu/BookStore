namespace BookStore.Data.Entities
{
    public class Book : BaseEntity<int>
    {
        public string BookName { get; set; }
        public List<Category> Categories { get; set; }

        public Author Author { get; set; }

    }
}
