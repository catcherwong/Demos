/**************************************************************************************
 *  ===              通过Microsoft.Owin.dll运行Nancy的应用示例                   ===
 * ==================================================================================
 * 目的：
 *   演示如果将 NancyFx 加入到 Microsoft.Owin 的处理环节中，然后利用Nancy建立web应用。
 * 使用方法：
 *   将编译得到的dll连同Owin.dll、Microsoft.Owin.dll、Nancy.dll、Nancy.Owin.dll等文件
 *   一并放置到网站的bin文件夹中
 *************************************************************************************/



#region <USINGs>

using System;
using System.Collections.Generic;
using Microsoft.Owin.Builder;
using System.Threading.Tasks;

#endregion



namespace OwinSelfDemo
{

    /// <summary>
    /// 针对Microsoft.Owin的JWS开放接口适配器
    /// <para>本接口适合兼容Microsoft.Owin规范的应用</para>
    /// </summary>
    class Adapter
    {
        static Func<IDictionary<string, object>, Task> _owinApp;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Adapter()
        {
            var builder = new AppBuilder();
            var startup = new Startup();
            startup.Configuration(builder);
            _owinApp = builder.Build();

        }


        /// <summary>
        /// *** JWS所需要的关键函数 ***
        /// <para>每个请求到来，JWS都会把请求打包成字典，调用这个函数</para>
        /// </summary>
        /// <param name="env">新请求的环境字典，具体内容参见OWIN标准</param>
        /// <returns>返回一个正在运行或已经完成的任务</returns>
        public Task OwinMain(IDictionary<string, object> env)
        {
            //如果为空
            if (_owinApp == null) return null;

            //将请求交给Microsoft.Owin处理
            return _owinApp(env);
        }


    } //end class


} //end namespace
