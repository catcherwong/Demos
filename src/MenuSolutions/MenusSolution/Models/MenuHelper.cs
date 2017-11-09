using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using System.IO;

namespace MenusSolution.Models
{
    public class MenuHelper
    {
        private static string connStr =$"DataSource ={ Path.Combine(Directory.GetCurrentDirectory(),"demo.db")}";        
        
        public static IList<Menu> GetAllMenuItems()
        {
            Console.WriteLine(connStr);
            using (IDbConnection conn = new SqliteConnection(connStr))
            {
                try
                {
                    conn.Open();
                    var sql = @"SELECT * FROM menu";
                    return conn.Query<Menu>(sql).ToList();
                }
                catch (Exception ex)
                {
                    return new List<Menu>();
                }
            }
        }

        public static IList<Menu> GetChildrenMenu(IList<Menu> menuList, string parentId = null)
        {
            return menuList.Where(x => x.ParentID == parentId).OrderBy(x => x.Order).ToList();
        }

        public static Menu GetMenuItem(IList<Menu> menuList, string id)
        {
            return menuList.FirstOrDefault(x => x.ID == id);
        }

    }
}
