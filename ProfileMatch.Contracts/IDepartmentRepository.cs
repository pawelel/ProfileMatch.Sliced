﻿using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        Task<Department> GetDepartment(int id);
        Task<IEnumerable<Department>> GetDepartmentsWithPeople();
    }
}
