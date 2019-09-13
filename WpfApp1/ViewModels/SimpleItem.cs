using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class SimpleItem<T>
    {
        public string Text { get; set; }
        public T Value { get; set; }
    }
}
