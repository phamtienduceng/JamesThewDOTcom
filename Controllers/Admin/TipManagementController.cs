using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class TipManagementController : Controller
{
    private readonly ITipManagementRepository _tipManagement;

    public TipManagementController(ITipManagementRepository tipManagement)
    {
        _tipManagement = tipManagement;
    }
}