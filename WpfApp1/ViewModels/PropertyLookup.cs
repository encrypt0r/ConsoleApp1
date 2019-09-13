using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class PropertyLookup
    {
        public string PropertyDisplayName { get; set; }
        public string PropertyName { get; set; }
        public IEnumerable<SimpleItem<string>> Lookup { get; set; }
    }
}
