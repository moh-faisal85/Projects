using Training.NETCoreMVC.DrinkAndGo.Data.Models;
using Training.NETCoreMVC.DrinkAndGo.interfaces;

namespace Training.NETCoreMVC.DrinkAndGo.Data.Mocks
{
    public class MockDrinkRepository : IDrinkRepository
    {
       private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();
        public IEnumerable<Drink> Drinks
        {
            get
            {
                return new List<Drink>
                {
                    new Drink
                    {
                        Name= "Beer",
                        Price = 7.95M,
                        ShortDescription="The most widely consumed alcohol",
                        LongDescription ="Beer is the world's oldest and most widely consumed alcoholic drink",
                        Category = _categoryRepository.Categories.First(),
                        ImageUrl = "https://fastpng.com/images/file/beer-png-timdn685mpvta7d8.png",
                        IntStock = 1,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl="https://fastpng.com/images/high/corona-beer-png-smitd40kvzac9l4n.webp"
                    },
                    new Drink
                    {
                        Name= "Rum & Cock",
                        Price = 7.95M,
                        ShortDescription="The most widely consumed alcohol",
                        LongDescription ="Beer is the world's oldest and most widely consumed alcoholic drink",
                        Category = _categoryRepository.Categories.First(),
                        ImageUrl = "https://fastpng.com/images/file/beer-png-timdn685mpvta7d8.png",
                        IntStock = 1,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl="https://fastpng.com/images/high/corona-beer-png-smitd40kvzac9l4n.webp"
                    }
                };
            }
        }

        public IEnumerable<Drink> PreferredDrinks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IEnumerable<Drink> IDrinkRepository.Drinks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Drink GetDrinkById(int drinkId)
        {
            return new Drink();
        }
    }
}
