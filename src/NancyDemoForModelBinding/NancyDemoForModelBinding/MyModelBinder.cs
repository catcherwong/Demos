//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Nancy;
//using Nancy.ModelBinding;
//using NancyModelBindingDemo.Models;

//namespace NancyDemoForModelBinding
//{
//    public class MyModelBinder : IModelBinder
//    {
//        public bool CanBind(Type modelType)
//        {
//            return modelType == typeof(Employee);
//        }


//        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
//        {
//            var employee = (instance as Employee) ?? new Employee();

//            employee.EmployeeName = context.Request.Form["EmployeeName"] ?? employee.EmployeeName;

//            employee.EmployeeNumber = context.Request.Form["EmployeeNumber"] ?? employee.EmployeeNumber;

//            employee.EmployeeAge = 24;//我们把年龄写死，方便看见差异 

//            employee.EmployeeHobby = ConvertStringToList(context.Request.Form["EmployeeHobby"]) ?? employee.EmployeeHobby;

//            return employee;
//        }


//        private List<string> ConvertStringToList(string input)
//        {
//            if (string.IsNullOrEmpty(input))
//            {
//                return null;
//            }
//            var items = input.Split(',');
//            return items.AsEnumerable().ToList<string>();
//        }
//    }
//}