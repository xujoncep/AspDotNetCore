using Microsoft.AspNetCore.Mvc;

namespace Pratices.Controllers
{
    public class LinqController: Controller
    {
        public IActionResult Linq()
        {
            //var data = _env.ContentRootFileProvider;
            //List<int> intList = new List<int>{ 1, 23, 34, 34, 3, 2, 4 };

            //var data = from item in intList 
            //           where(item % 2 == 0)
            //           select item;

            //var data2 = intList.Where( i => i % 2 == 0).ToArray();

            //var data3 = (from item in _context.Branches
            //            where(item.IsActive == true)
            //            select item).ToList();

            //var data4 = intList.Distinct().Count();
            //var data5 = (from item in _context.Banks
            //            select new Bank() {
            //             BankId = item.BankId,
            //             BankName = item.BankName,
            //            }).ToList();

            //var data6 = _context.Customers.FromSqlRaw("select count(*) from Customers");
            //List<object> data7 = new List<object> {12,"name",true,"age" };

            //var data8 = data7.OfType<int>().ToList(); // (from number in data7.OfType<int>()
            //                                          // select number).ToArry();

            //int[] intArry1 = {1,2,3,4,5,3,23,43,3,2,4};
            //int[] intArry2 = { 2, 4, 3, 4, 23, 5, 3, 23, 8, 6, 7 };

            //var result = intArry1.Except(intArry2);

            string[] skills = { "C#.NET", "MVC", "WCF", "SQL", "LINQ", "ASP.NET" };
            string result = skills.Aggregate((item1, item2) => item1 + ", " + item2);

            int[] intNumbers = { 3, 5, 7, 9 };
            int result1 = intNumbers.Aggregate((n1, n2) => n1 * n2);

            return View();
        }
    }
}
