using System;
using System.Collections.Generic;

namespace vaultn_example_app.Models
{
    public class TransactionResponse
    {
        public string Guid { get; set; }
        public string ClientReference { get; set; }
        public DateTime Created { get; set; }
        public int StatusID { get; set; }
        public int TypeID { get; set; }
        public string ProductGUID { get; set; }
        public string ProductTitle { get; set; }
        public string AgreementGUID { get; set; }
        public string AgreementName { get; set; }
        public int SerialCount { get; set; }
        public List<Serial> Serials { get; set; }
    }
}