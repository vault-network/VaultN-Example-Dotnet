using System.Collections.Generic;

namespace vaultn_example_app.Models
{
    public class ProductRequest
    {
        public string Title { get; set; }
        public int ProductTypeID { get; set; }
        public List<KeyValuePair<string,object>> MetaData { get; set; }
    }
}