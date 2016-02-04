using Nancy;
using Nancy.ModelBinding;
using NancyDemoForModelBinding.Models;
using System.Text;

namespace NancyDemoForModelBinding.Modules
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/default"] = _ =>
            {               
                return View["default"];
            };

            Post["/default"] = _ =>
            {
                Employee employee_Empty = new Employee();

                //这种写法有问题，应该是 Employee xxx = this.Bind(); 才对！
                //因为这里的this.Bind() 是 dynamic 类型，没有直接指明类型
                //所以它会提示我们  “找不到此对象的进一步信息”
                var employee_Using_Bind = this.Bind();
                
                //这里在bind的时候指明了类型。这个会正常绑定数据。(推荐这种写法)
                var employee_Using_BindWithTModel = this.Bind<Employee>();

                //这里是将数据绑定到我们实例化的那个employee_Empty对象
                //运行到这里之后，会发现employee_Empty的默认值被替换了！！
                var employee_Using_BindTo = this.BindTo(employee_Empty);

                //与上面的写法等价！
                var employee_Using_BindToWithTModel = this.BindTo<Employee>(employee_Empty);

                //这个主要是演示“黑名单”的用法，就是绑定数据的时候忽略某几个东西
                //这里忽略了EmployeeName和EmployeeAge，所以得到的最终还是我们设置的默认值
                var employee_Using_BindAndBlacklistStyle1 = this.Bind<Employee>(e=>e.EmployeeName,e=>e.EmployeeAge);

                //与上面的写法等价，演示不同的写法而已！          
                var employee_Using_BindAndBlacklistStyle2 = this.Bind<Employee>("EmployeeName", "EmployeeAge");

                return Response.AsRedirect("/default");
            };


            Get["/custom"] = _ =>
            {
                return View["custom"];
            };

            Post["/custom"] = x =>
            {
                //此时就会调用我们自己定义的Binder了
                var employee1 = this.Bind<Employee>();
                Employee employee2 = this.Bind();              

                return Response.AsRedirect("/custom");
            };

            Get["/json"] = _ =>
            {
                return View["json"];
            };

            Post["/json"] = _ =>
            {

                var employee = this.Bind<Employee>();

                var sb = new StringBuilder();
                sb.AppendLine("绑定的employee的值:");
                sb.Append("编号: ");
                sb.AppendLine(employee.EmployeeNumber);
                sb.Append("姓名: ");
                sb.AppendLine(employee.EmployeeName);
                sb.Append("年龄: ");
                sb.AppendLine(employee.EmployeeAge.ToString());                

                return sb.ToString();
            };
        }
    }
}