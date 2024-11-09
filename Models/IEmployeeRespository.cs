using System.Collections.Generic;

namespace Kudvenkat_.NET_Core.Models
{
    public interface IEmployeeRespository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetEmployees();

        Employee Add(Employee employee);

        Employee Update(Employee employee);

        Employee Delete(int Id);

    }
}
