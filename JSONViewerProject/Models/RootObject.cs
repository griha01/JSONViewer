using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONViewerProject.Model
{
    class RootObject
    {
        public List<PlanItemProductItemRelation> PlanItemProductItemRelations { get; set; }
        public List<ProductItemProductItemRelation> ProductItemProductItemRelations { get; set; }
        public List<Product> Products { get; set; }
    }
}
