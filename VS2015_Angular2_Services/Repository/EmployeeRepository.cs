using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS2015_Angular2_Services.Models;

namespace VS2015_Angular2_Services.Repository
{
    public class EmployeeRepository
    {
        private ProjectDBContext _context;
        const int pageSize = 10;

        public EmployeeRepository()
        {
            _context = new ProjectDBContext();
        }

        //public List<Employee> GetAllEmployees()
        //{
        //    return _context.Employees.ToList();
        //}

        public List<Employee> SearchEmployees(string srch)
        {
            IQueryable<Employee> result = _context.Employees;

            if (!string.IsNullOrEmpty(srch))
            {
                result = result
                        .Where(w => w.EmployeeName.ToLower().Contains(srch.ToLower())
                                || w.Designation.ToLower().Contains(srch.ToLower())
                                || w.EMailId.ToLower().Contains(srch.ToLower())
                                || w.ContactNo.ToLower().Contains(srch.ToLower())
                                || w.SkillSets.ToLower().Contains(srch.ToLower()));
            }

            return result.ToList();
        }

        public Employee GetEmployee(int EmployeeID)
        {
            return _context.Employees.Where(w => w.ID == EmployeeID).FirstOrDefault();
        }

        public List<ManagerViewModel> GetManagers()
        {
            return _context.Employees
                    .Select(s => new ManagerViewModel
                    {
                        ManagerID = s.ID,
                        ManagerName = s.EmployeeName
                    })
                    .ToList();
        }

        public List<EmployeeDDLViewModel> GetAllForDdl()
        {
            return _context.Employees.Select(s => new EmployeeDDLViewModel { ID = s.ID, EmployeeName = s.EmployeeName }).ToList();
        }

        public List<EmployeeViewModel> GetAllEmployees(string srch, int page = 1, int pagesize = pageSize)
        {
            IQueryable<EmployeeViewModel> result = _context.Employees
                                                    .GroupJoin(_context.Employees, 
                                                                a => a.ManagerID, 
                                                                b => b.ID, 
                                                                (p, q) => new { emp = p, manager = q })
                                                    .SelectMany(x => x.manager.DefaultIfEmpty(),
                                                    (e, m) => new EmployeeViewModel
                                                    {
                                                        ContactNo = e.emp.ContactNo,
                                                        Designation = e.emp.Designation,
                                                        EMailId = e.emp.EMailId,
                                                        EmployeeName = e.emp.EmployeeName,
                                                        ID = e.emp.ID,
                                                        ManagerID = e.emp.ManagerID,
                                                        ManagerName = m.EmployeeName,
                                                        SkillSets = e.emp.SkillSets,
                                                        ImagePath = e.emp.ImagePath,
                                                    })
                                                    .Distinct();

            if (!string.IsNullOrEmpty(srch))
            {
                result = result
                        .Where(w => w.EmployeeName.ToLower().Contains(srch.ToLower())
                                || w.Designation.ToLower().Contains(srch.ToLower())
                                || w.EMailId.ToLower().Contains(srch.ToLower())
                                || w.ContactNo.ToLower().Contains(srch.ToLower())
                                || w.SkillSets.ToLower().Contains(srch.ToLower()));
            }

            var filteredResult = result
                                .OrderBy(o => o.ManagerName)
                                .ThenBy(t => t.EmployeeName)
                                .Skip((page - 1) * pagesize)
                                .Take(pagesize)
                                .ToList();

            return filteredResult;
        }

        public List<EmployeeViewModel> GetEmployeesByPaging(int page = 1, int pagesize = pageSize)
        {
            var result = _context.Employees
                            .GroupJoin(_context.Employees, a => a.ManagerID, b => b.ID, (p, q) => new { emp = p, manager = q })
                            .SelectMany(x => x.manager.DefaultIfEmpty(),
                            (e, m) => new EmployeeViewModel
                            {
                                ContactNo = e.emp.ContactNo,
                                Designation = e.emp.Designation,
                                EMailId = e.emp.EMailId,
                                EmployeeName = e.emp.EmployeeName,
                                ID = e.emp.ID,
                                ManagerID = e.emp.ManagerID,
                                ManagerName = m.EmployeeName,
                                SkillSets = e.emp.SkillSets,
                                ImagePath = e.emp.ImagePath,
                            })
                            .Distinct()
                            .OrderBy(o => o.ManagerName)
                            .ThenBy(t => t.EmployeeName)
                            .Skip((page - 1) * pagesize)
                            .Take(pagesize)
                            .ToList();

            return result;
        }

        public List<EmployeeViewModel> InsertEmployee(Employee e)
        {
            _context.Employees.Add(e);
            _context.SaveChanges();
            return GetAllEmployees("", 1, pageSize);
        }

        public List<EmployeeViewModel> UpdateEmployee(Employee emp)
        {
            _context.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return GetAllEmployees("", 1, pageSize);
        }

        public List<EmployeeViewModel> DeleteEmployee(int id)
        {
            var emp = _context.Employees.Where(w => w.ID == id).FirstOrDefault();
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return GetAllEmployees("", 1, pageSize);
        }

        public void Detach(Employee emp)
        {
            _context.Entry(emp).State = System.Data.Entity.EntityState.Detached;
        }
    }
}