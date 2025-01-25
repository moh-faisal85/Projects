using Training.NETCoreMVC.DrinkAndGo.Data.Models;
using Training.NETCoreMVC.DrinkAndGo.interfaces;

namespace Training.NETCoreMVC.DrinkAndGo.Data.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category{CategoryName="Alcoholic", Description="All Alcoholic Drinks"},
                    new Category{CategoryName="Non-Alcoholic", Description="All Non-Alcoholic Drinks"}
                };
            }
        }
    }
}
