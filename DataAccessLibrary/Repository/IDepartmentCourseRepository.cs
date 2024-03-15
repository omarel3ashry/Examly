using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository
{
    public interface IDepartmentCourseRepository: IRepository<DepartmentCourse>
    {
        public List<Course> GetCoursesByDeptIdWithIncludes(int deptId);
        public DepartmentCourse? GetByDeptAndCrsIdWithIncludes(int crsId, int deptId);

    }
}
