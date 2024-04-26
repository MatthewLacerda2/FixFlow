using System.Text;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utils;
namespace Server.Seeder;

public class FlowSeeder
{
    private readonly ServerContext _context;

    private readonly UserManager<Client> _clientUserManager;
    private readonly UserManager<Employee> _employeeUserManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    const int employeesCount = 100;
    const int clientsCount = employeesCount * 100;

    public FlowSeeder(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<ServerContext>();
        _clientUserManager = serviceProvider.GetRequiredService<UserManager<Client>>();
        _employeeUserManager = serviceProvider.GetRequiredService<UserManager<Employee>>();
        _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    }

    async void PopulateRoles()
    {

        var clientRoleExist = await _roleManager.RoleExistsAsync(Common.Client_Role);
        if (!clientRoleExist)
        {
            await _roleManager.CreateAsync(new IdentityRole(Common.Client_Role));
        }

        var employeeRoleExist = await _roleManager.RoleExistsAsync(Common.Employee_Role);
        if (!employeeRoleExist)
        {
            await _roleManager.CreateAsync(new IdentityRole(Common.Employee_Role));
        }

    }

    async void PopulateEmployees()
    {

        if (_context.Employees.Any())
        {
            return;
        }

        var employeeSeed = new EmployeeSeed();
        var employees = Enumerable.Range(1, employeesCount).Select(_ => employeeSeed.Generate()).ToArray();

        foreach (var employee in employees)
        {
            var userCreationResult = await _employeeUserManager.CreateAsync(employee, "@Bogus1234");

            if (userCreationResult.Succeeded)
            {
                var userRoleAddResult = await _employeeUserManager.AddToRoleAsync(employee, Common.Client_Role);
            }
        }

    }

    async void PopulateClients()
    {
        if (!_context.Clients.Any())
        {
            return;
        }

        var clientSeed = new ClientSeed();
        var clients = Enumerable.Range(1, clientsCount).Select(_ => clientSeed.Generate()).ToArray();

        foreach (var client in clients)
        {
            var userCreationResult = await _clientUserManager.CreateAsync(client, "@Bogus1234");
            if (userCreationResult.Succeeded)
            {
                var userRoleAddResult = await _clientUserManager.AddToRoleAsync(client, Common.Client_Role);
            }
        }

    }

    public async void PopulateDbIfEmpty()
    {

        PopulateRoles();

        PopulateClients();

        await _context.SaveChangesAsync();
    }
}