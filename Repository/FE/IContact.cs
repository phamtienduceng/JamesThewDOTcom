using JamesRecipes.Models;
using model.Models;

namespace JamesRecipes.Repository.FE;

public interface IContact
{
    void SendContact(Contact newContact);
}