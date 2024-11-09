using System.Collections.Generic;

namespace Kudvenkat_.NET_Core.Models
{
    public class SQLEmployeeRepository : IEmployeeRespository
    {
        private readonly AppDBContext context;
        public SQLEmployeeRepository(AppDBContext context)
        {
            this.context = context;

        }
        public SQLEmployeeRepository() { }

        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees;
        }

        public Employee Update(Employee employee)
        {
            var emp = context.Employees.Attach(employee);
            if (emp != null)
            {
                emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            return employee;
        }


    }
}
