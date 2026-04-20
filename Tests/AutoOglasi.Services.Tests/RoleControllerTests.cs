namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleController = AutoOglasi.Web.Areas.Admin.Controllers.RoleController;

public class RoleControllerTests
{
    [Fact]
    public void Index_ReturnsRolesView()
    {
        var roleManager = TestIdentityManagers.CreateRoleManager(new IdentityRole("Administrator"), new IdentityRole("Moderator"));
        var controller = new RoleController(roleManager, TestIdentityManagers.CreateUserManager(Array.Empty<ApplicationUser>()));

        var result = controller.Index();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_RedirectsToIndex_WhenRoleIsCreated()
    {
        var roleManager = TestIdentityManagers.CreateRoleManager();
        var controller = new RoleController(roleManager, TestIdentityManagers.CreateUserManager(Array.Empty<ApplicationUser>()));

        var result = await controller.Create("Moderator");

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public async Task Delete_ReturnsIndexView_WhenRoleDoesNotExist()
    {
        var controller = new RoleController(
            TestIdentityManagers.CreateRoleManager(),
            TestIdentityManagers.CreateUserManager(Array.Empty<ApplicationUser>()));

        var result = await controller.Delete("missing-role");

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        Assert.False(controller.ModelState.IsValid);
    }
}
