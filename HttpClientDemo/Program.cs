using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var goRestClient = new GoRestClient();

            List<User> users = await goRestClient.GetUsersAsync();

            Console.WriteLine("All users retrieved by Get Request.");
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.name} email: {user.email}");
            }
            Console.WriteLine("----------------------------------------------------");

            User createdUser = await goRestClient.CreateUserAsync();
            Console.WriteLine($"Created user name: {createdUser.name} email : {createdUser.email}");
            Console.WriteLine("----------------------------------------------------");

            User updateUser = await goRestClient.UpdateUserAsync();
            Console.WriteLine($"Updated user Name: {updateUser.name} Email : {updateUser.email}");
            Console.WriteLine("----------------------------------------------------");

            await goRestClient.DeleteUsersAsync();
            Console.WriteLine($"User with Id: {1633} is successfully deleted.");
            Console.WriteLine("----------------------------------------------------");

        }

    }
}
