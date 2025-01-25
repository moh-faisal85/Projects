using Training.NETCoreMVC.DrinkAndGo.Data.Models;

namespace Training.NETCoreMVC.DrinkAndGo.interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
