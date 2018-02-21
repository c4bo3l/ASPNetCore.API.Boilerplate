using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetCore.API.Boilerplate.Models
{
    public static class DBInitializer
    {
        public static async void Initialize(MyDBContext context) {
            if (context == null)
                throw new ArgumentNullException("context is null");

            bool isDirty = false;

            List<EmployeeModel> employees = context.Employees.ToList();
            if (!context.Employees.Any())
            {
                Random rand = new Random();
                for (int i = 1; i <= 30; i++)
                {
                    EmployeeModel model = new EmployeeModel()
                    {
                        EmployeeID = string.Format("E{0}", i),
                        FirstName = string.Format("FirstName{0}", i),
                        LastName = string.Format("LastName{0}", i),
                        DateOfBirth = new DateTime(rand.Next(1980, 2001), rand.Next(1, 13), rand.Next(1, 28))
                    };
                    context.Add<EmployeeModel>(model);
                    employees.Add(model);
                }

                isDirty = true;
            }

            if (!context.Companies.Any())
            {
                string[] guids = new string[] {
                    "bc5bc6b1-03ed-4a89-ba15-75c7f5370f62",
                    "31df116d-ff6f-4dd9-bf10-8f8043b3a160",
                    "4b5e3ac5-91df-4d2d-b75f-e65598a2f163",
                    "438635cd-112b-453e-a6e9-dba92cfdd841",
                    "5a26970f-dfcd-4dfa-8195-b48fa9ec71f1",
                    "8b5d8a21-4bee-4fdf-9488-fffb1d23ac3c",
                    "ce28b7b1-c15d-4b1a-8d7b-679c797cb57c",
                    "ffd9b684-3f1d-4466-884b-875de4c6ffd9",
                    "02870e06-2a86-427a-8a8f-fed9d3051046",
                    "f4954e07-06e6-4f29-a8b9-75c3b0368774"
                };

                for (int i = 0; i < guids.Length; i++)
                {
                    CompanyModel model = new CompanyModel()
                    {
                        CompanyID = new Guid(guids[i]),
                        Name = string.Format("Company-{0}", i + 1),
                        Employees = new List<EmployeeModel>()
                    };

                    for (int j = i * 3; j < (i*3) + 3; j++)
                    {
                        model.Employees.Add(employees[j]);
                    }
                    context.Add<CompanyModel>(model);
                }

                isDirty = true;
            }

            if (isDirty)
                await context.SaveChangesAsync();
        }
    }
}
