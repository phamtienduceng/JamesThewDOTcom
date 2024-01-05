using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IContact
{
    void SendContact(Contact newContact);
}