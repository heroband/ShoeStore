using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Models.Entities;

namespace ShoeStore.Models.ViewModels
{
    public class SneakersDetailsViewModel
    {
        public Sneakers Sneakers { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public double AverageRating { get; set; }
        public int? UserRating { get; set; }
    }
}
