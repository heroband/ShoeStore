using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Models.ViewModels
{
    public class FilterViewModel
    {
        public IEnumerable<SneakersShortInfoViewModel> Sneakers { get; set; }
        public List<string> SelectedBrands { get; set; } = new();
    }
}
