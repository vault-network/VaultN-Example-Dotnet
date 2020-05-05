namespace vaultn_example_app.Models
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public int ProductTypeID { get; set; }
    }
}