using Kudvenkat_.NET_Core.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Kudvenkat_.NET_Core.Models
{
    public static class ModelBuilderExtension
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                 new Employee
                 {
                     Id = 1,
                     Name = "Ram",
                     Email = "ram@gmail.com",
                     Department = Dept.IT,
                     Photopath = ""
                 },
                  new Employee
                  {
                      Id = 2,
                      Name = "Sham",
                      Email = "sham@gmail.com",
                      Department = Dept.Payroll,
                      Photopath = ""
                  },
                   new Employee
                   {
                       Id = 3,
                       Name = "Shiv",
                       Email = "shiv@gmail.com",
                       Department = Dept.HR,
                       Photopath = ""
                   },
                   new Employee
                   {
                       Id = 10,
                       Name = "Raj",
                       Email = "raj@gmail.com",
                       Department = Dept.HR,
                       Photopath = ""
                   }

                );
        }
    }
}
