using System;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace MovieDemoWithOwin
{


    /// <summary>
    /// Razor配置，如果你需要使用 cshtml，这个配置比较重要，当然，也可以在这儿加入其它的类
    /// </summary>
    public class RazorConfig : IRazorConfiguration
    {

        /// <summary>
        /// 需加载的程序集列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAssemblyNames()
        {
            //加了这句，才能处理 cshtml
            yield return "System.Web.Razor";
        }

        /// <summary>
        /// 需要添加到cshtml中的名字空间
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "System.Web.Razor";
        }

        /// <summary>
        /// 是否自动引用model名字空间
        /// </summary>
        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }


}
