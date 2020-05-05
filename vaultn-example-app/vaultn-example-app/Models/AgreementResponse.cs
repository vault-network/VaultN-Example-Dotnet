using System;
using System.Collections.Generic;

namespace vaultn_example_app.Models
{
    public class AgreementResponse
    {
        public string Guid { get; set; }
        public string OwnerGuid { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int GeofencingTypeID { get; set; }
        public bool IsBlacklistingAllowed { get; set; }
        public int VisibilityTypeID { get; set; }
        public int ExtractionTypeID { get; set; }
        public List<string> Countries { get; set; }

    }
}