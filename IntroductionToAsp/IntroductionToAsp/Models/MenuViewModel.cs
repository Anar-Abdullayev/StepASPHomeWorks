using IntroductionToAsp.Entities;
using System.Collections.Generic;

namespace IntroductionToAsp.Models
{
    public class MenuViewModel
    {
        public List<Drink> Drinks { get; set; }
        public List<FastFood> FastFoods { get; set; }
        public List<HotMeal> HotMeals { get; set; }
    }
}
