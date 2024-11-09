using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kudvenkat_.NET_Core.Models
{
    public class MockEmployeeRespository : IEmployeeRespository
    {

        private List<Employee> _employeesList;

        public MockEmployeeRespository()
        {
            _employeesList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Mary",
                    Department = Dept.HR,
                    Email = "mary@gmail.com"
                },

                new Employee()
               {
                    Id = 2,
                    Name = "Jack",
                    Department = Dept.IT,
                    Email = "jack@gmail.com"
               }

             };
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeesList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeesList.FirstOrDefault(x => x.Id == Id);

        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeesList.Max(x => x.Id);
            _employeesList.Add(employee);
            return employee;
        }

        public Employee Update(Employee employee)
        {
            Employee emp = _employeesList.FirstOrDefault(x => x.Id == employee.Id);

            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Email = employee.Email;
                emp.Department = employee.Department;
            }
            return emp;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _employeesList.FirstOrDefault(x => x.Id == Id);

            if(employee != null)
            {
                _employeesList.Remove(employee);
            }
            return employee;
        }
    }
}
