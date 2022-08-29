using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmployeeManager:IEmployeeService
    {
        private IEmployeeDal _employeeDal;
        public EmployeeManager(IEmployeeDal employeeeDal)
        {
            _employeeDal = employeeeDal;
        }
        public IDataResult<List<Employee>> GetAll()
        {
            var result = _employeeDal.GetAll();
            if (result == null)
                return new ErrorDataResult<List<Employee>>("Çalışanlar listelenemedi!");

            return new SuccessDataResult<List<Employee>>(result.ToList());
        }

        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Add(Employee employee)
        {
           IResult result= BusinessRules.Run(CheckNameExist(employee.FirstName),  //İş kuralları
                CheckEmployeeCount());
            if (result != null)
            {
                return result;
            }

            _employeeDal.Add(employee);
            return new SuccessResult(Messages.EmployeeAdded);
      

        }

        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Update(Employee employee)
        {
            IResult result = BusinessRules.Run(CheckNameExist(employee.FirstName),  //İş kuralları
                CheckEmployeeCount());
            if (result != null)
            {
                return result;
            }

            _employeeDal.Update(employee);
            return new SuccessResult("Çalışan bilgisi güncellendi");
        }

        public IResult Delete(Employee employee)
        {
           
            _employeeDal.Delete(employee);
            return new SuccessResult("Çalışan silindi");
        }

        private IResult CheckEmployeeCount()
        {
            var result = _employeeDal.GetAll().Count;
            if (result > 150)
            {
                return new ErrorResult(Messages.EmployeeCountError);
            }
            return new SuccessResult();
        }

        private IResult CheckNameExist(string name)
        {
            var result = _employeeDal.GetAll(p=>p.FirstName==name).Count;
            if (result > 0)
            {
                return new ErrorResult(Messages.EmployeeNameExistError);
            }
            return new SuccessResult();
        }
    }
}
