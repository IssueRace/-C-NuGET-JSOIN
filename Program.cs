using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp4
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class Admin : User
    {
        public string Role => "Admin";
    }

    public class RegularUser : User
    {
        public string Role => "User";
    }

    public class UserWrapper
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string singlePath = "user.json";
            string singleJson = File.ReadAllText(singlePath);
            User singleUser = JsonConvert.DeserializeObject<User>(singleJson);
            Console.WriteLine($"Single user: {singleUser.Name}, {singleUser.Age}, {singleUser.City}");

            string listPath = "users.json";
            string listJson = File.ReadAllText(listPath);
            List<UserWrapper> rawUsers = JsonConvert.DeserializeObject<List<UserWrapper>>(listJson);
            List<User> realUsers = new();

            foreach (var u in rawUsers)
            {
                if (u.Type == "Admin")
                    realUsers.Add(new Admin { Name = u.Name, Age = u.Age, City = u.City });
                else
                    realUsers.Add(new RegularUser { Name = u.Name, Age = u.Age, City = u.City });
            }

            Console.WriteLine("\nDeserialized users:");
            foreach (var user in realUsers)
            {
                string role = user is Admin ? "Admin" : "User";
                Console.WriteLine($"{role}: {user.Name}, {user.Age}, {user.City}");
            }

            var newUsers = new List<UserWrapper>
            {
                new UserWrapper { Name = "Eve", Age = 29, City = "Paris", Type = "User" },
                new UserWrapper { Name = "Charlie", Age = 31, City = "Madrid", Type = "Admin" }
            };

            string newJson = JsonConvert.SerializeObject(newUsers, Formatting.Indented);
            File.WriteAllText("created_users.json", newJson);
            Console.WriteLine("\nNew users added and saved to created_users.json.");
        }
    }
}
