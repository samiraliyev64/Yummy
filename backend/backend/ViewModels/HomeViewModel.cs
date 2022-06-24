using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.ViewModels
{
    public class HomeViewModel
    {
        public FoodSummary FoodSummary { get; set; }
        public List<Product> Products { get; set; }
        public BookTable BookTable { get; set; }
    }
}
