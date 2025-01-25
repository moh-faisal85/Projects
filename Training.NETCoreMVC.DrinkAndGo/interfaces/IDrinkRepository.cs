using Training.NETCoreMVC.DrinkAndGo.Data.Models;

namespace Training.NETCoreMVC.DrinkAndGo.interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> Drinks { get; set; }

        IEnumerable<Drink> PreferredDrinks { get; set; }
        Drink GetDrinkById(int drinkId);
    }
}
