using System.Text;
using Newtonsoft.Json;
using Server.Data;
namespace Server.Seeder;

public class FlowSeeder
{
    private readonly ServerContext _context;
    private readonly HttpClient _httpClient;

    public FlowSeeder(ServerContext context)
    {
        _context = context;
        _httpClient = new HttpClient();
    }

    public async void PopulateDbIfEmpty()
    {

        int employeesCount = 100;
        int clientsCount = employeesCount * 100;

        if (!_context.Employees.Any())
        {
            var employeeSeed = new EmployeeSeed();
            var employees = Enumerable.Range(1, employeesCount).Select(_ => employeeSeed.Generate()).ToArray();

            foreach (var employee in employees)
            {
                var json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("http://localhost:5105/api/v1/employee", content);
            }

        }

        if (!_context.Clients.Any())
        {
            var clientSeed = new ClientSeed();
            var clients = Enumerable.Range(1, clientsCount).Select(_ => clientSeed.Generate()).ToArray();

            foreach (var client in clients)
            {
                var json = JsonConvert.SerializeObject(client);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("http://localhost:5105/api/v1/client", content);
            }
        }
    }
}