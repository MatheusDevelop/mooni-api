using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public int IconId { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
    }
}
