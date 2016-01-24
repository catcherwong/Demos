/******************************************************************
 * 为NancyFx提供应用程序根目录绝对路径的类
 * ---------------------------------------------------------------
 * 要点：类名可以随便取，但必需继续自 IRootPathProvider
 *       整个应用程序（网站）只能有一个这样的类
 * ****************************************************************/


#region <USINGs>

using System;
using Nancy;
using System.IO;

#endregion


namespace MovieDemoWithOwin
{

    /// <summary>
    /// 提供网站物理路径的类
    /// </summary>
    public class SiteRootPath : IRootPathProvider
    {

        /**************************************************************
         * TinyFox Owin Server 默认情况下
         * 网站是放在 TinyFox 进程所在文件夹下的site/wwwroot中的
         * ----------------------------------------------------------
         * 如果你把 NancyFx 的 Views 页放在其它的地方，应该作相应修改
         *******************************************************************/

        /// <summary>
        /// 网站根文件夹物理路径(for tinyfox)
        /// </summary>
        static readonly string _RootPath = AppDomain.CurrentDomain.GetData(".appPath").ToString();


        /// <summary>
        /// 获取网站或WEB应用的根文件夹的物理路径
        /// </summary>
        /// <returns></returns>
        public string GetRootPath()
        {
            return _RootPath;

        }

    }
}
