using Business.Abstract;
using Business.BussinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Transaction;
using Core.CrossCuttingConcerns.Caching;
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

        [CacheAspect]
        public IDataResult<List<Employee>> GetAll()
        {
            var result = _employeeDal.GetAll();
            if (result == null)
                return new ErrorDataResult<List<Employee>>("Çalışanlar listelenemedi!");

            return new SuccessDataResult<List<Employee>>(result.ToList());
        }


       
        [CacheRemoveAspect("IEmployeeService.Get")]
        [SecuredOperation("Employee.Add,Admin")]
        [TransactionScopeAspect]
        [PerformanceAspect(5)]   //5 saniyeden uzun sürüyorsa uyarı verecek şekilde ayarlandı.
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Add(Employee employee)
        {
           IResult result= BusinessRules.Run(CheckNameExist(employee.FirstName),CheckEmployeeCount());
            if (result != null)
            {
                return result;
            }

            _employeeDal.Add(employee);
            return new SuccessResult(Messages.EmployeeAdded);
      

        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("IEmployeeService.Update")]
        [ValidationAspect(typeof(EmployeeValidator))]
        [PerformanceAspect(5)]
        public IResult Update(Employee employee)
        {
            IResult result = BusinessRules.Run(CheckNameExist(employee.FirstName), CheckEmployeeCount());
            if (result != null)
            {
                return result;
            }

            _employeeDal.Update(employee);
            return new SuccessResult(Messages.EmployeeUpdated);
        }


        [TransactionScopeAspect]
        [CacheRemoveAspect("IEmployeeService.Delete")]
        [PerformanceAspect(5)]
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
