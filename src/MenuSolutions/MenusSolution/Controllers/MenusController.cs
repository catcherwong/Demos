using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using MenusSolution.Models;

namespace MenusSolution.Controllers
{
    public class MenusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RenderString()
        {
            return View("String", GetMenuString());
        }
        
        public IActionResult Model()
        {
            var menuItems = MenuHelper.GetAllMenuItems();
            return View("Model", GetMenu(menuItems, null));
        }

        private string GetMenuString()
        {
            var menuItems =  MenuHelper.GetAllMenuItems();

            var builder = new StringBuilder();
            builder.Append("<ul class=\"sidebar-menu\" data-widget=\"tree\">");
            builder.Append(GetMenuLiString(menuItems, null));
            builder.Append("</ul>");
            return builder.ToString();
        }

        private string GetMenuLiString(IList<Menu> menuList, string parentId)
        {
            var children = MenuHelper.GetChildrenMenu(menuList, parentId);

            if (children.Count <= 0)
            {
                return "";
            }

            var builder = new StringBuilder();

            foreach (var item in children)
            {
                var childStr = GetMenuLiString(menuList, item.ID);
                if (!string.IsNullOrWhiteSpace(childStr))
                {
                    builder.Append("<li class=\"treeview\">");
                    builder.Append("<a href=\"#\">");
                    builder.AppendFormat("<i class=\"{0}\"></i> <span>{1}</span>", item.IconClass, item.Content);
                    builder.Append("<span class=\"pull-right-container\">");
                    builder.Append("<i class=\"fa fa-angle-left pull-right\"></i>");
                    builder.Append("</span>");
                    builder.Append("</a>");
                    builder.Append("<ul class=\"treeview-menu\">");
                    builder.Append(childStr);
                    builder.Append("</ul>");
                    builder.Append("</li>");
                }
                else
                {
                    builder.Append("<li class=\"treeview\">");
                    builder.AppendFormat("<a href=\"{0}\">", item.Href);
                    builder.AppendFormat("<i class=\"{0}\"></i> <span>{1}</span>", item.IconClass, item.Content);
                    builder.Append("</a>");
                    builder.Append("</li>");
                }
            }

            return builder.ToString();
        }

        private IList<MenuViewModel> GetMenu(IList<Menu> menuList, string parentId)
        {
            var children = MenuHelper.GetChildrenMenu(menuList, parentId);

            if (!children.Any())
            {
                return new List<MenuViewModel>();
            }

            var vmList = new List<MenuViewModel>();
            foreach (var item in children)
            {
                var menu = MenuHelper.GetMenuItem(menuList, item.ID);

                var vm = new MenuViewModel();

                vm.ID = menu.ID;
                vm.Content = menu.Content;
                vm.IconClass = menu.IconClass;
                vm.Href = menu.Href;
                vm.Children = GetMenu(menuList, menu.ID);

                vmList.Add(vm);
            }

            return vmList;
        }
    }
}