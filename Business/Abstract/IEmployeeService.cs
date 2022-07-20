using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        public IDataResult<List<Employee>> GetAll();
        public IResult Add(Employee employee);
        public IResult Update(Employee employee);
        public IResult Delete(Employee employee);
    }
}
