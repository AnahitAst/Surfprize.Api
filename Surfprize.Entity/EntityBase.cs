using System;
using System.Collections.Generic;
using System.Text;

namespace Surfprize.Entity
{
    public abstract class EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
