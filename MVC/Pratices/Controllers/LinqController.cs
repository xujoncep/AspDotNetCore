using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;

namespace Pratices.Controllers
{
    public class LinqController: Controller
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public int Age { get; set; }

        }

        public class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public string Address { get; set; }
        }

        public IActionResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Jhon", Age = 12},
                new Customer { Id = 2, Name = "Bob", Age = 15},
                new Customer { Id = 3, Name = "Alice", Age = 20}
            };

            var orders = new List<Order>
            {
                new Order { OrderId = 101, CustomerId = 1, Address = "Karwan bazar, dhaka" },
                new Order { OrderId = 102, CustomerId = 2, Address = "Mirpur, dhaka" },
                new Order { OrderId = 103, CustomerId = 1, Address = "Badda, dhaka" },
                new Order { OrderId = 104, CustomerId = 3, Address = "DhanMondi, dhaka" }
            };

            //var result = customers.Where(c => c.Age > 12 && c.Age < 30);

            //var arrayList = new ArrayList() {123,23,true, false,"alice"};
            //arrayList.Add(true);
            //arrayList.Add("Jhon");
            //arrayList.Add(12);
            //var result = arrayList.OfType<int>();

            //var result = orders.OrderBy( d => d.OrderId ).ThenByDescending( d => d.Address).ToList();

            //var result = customers.Aggregate<Customer,string>("Names ",(str, item) => str + item.Name + ", ");
            //var totalAge = customers.Aggregate("Total Age:",(age,item) => age = age + item.Age);
            //var totalAge = customers.Aggregate(0, (age, item) => age = age + item.Age);
            //var avgAge = customers.Average(x => x.Age);

            //var strList1 = new List<string>() { "one", "two", "three", "four" };
            //var strList2 = new List<string>() { "four", "five" };

            //var result = strList1.Concat(strList2);
            //var result2 = strList1.Union(strList2);
            //var result3 = strList1.Except(strList2);
            //var result4 = strList1.Intersect(strList2);
            
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

            //string[] skills = { "C#.NET", "MVC", "WCF", "SQL", "LINQ", "ASP.NET" };
            //string result = skills.Aggregate((item1, item2) => item1 + ", " + item2);

            //int[] intNumbers = { 3, 5, 7, 9 };
            //int result1 = intNumbers.Aggregate((n1, n2) => n1 * n2);

            //operation on number
            //var numbers = new List<int>() {1,2,3,4,5,6,7,8,3,3,4,2 };
            //var numbers2 = new List<int>() { 2, 4, 6, 8 };
            //var numbers3 = new List<int>() { 3, 6, 7, 9 };

            //var distinctNumber = numbers.Distinct().ToList();

            //var evenNumber = numbers.Where(x => x % 2 == 0).ToList();
            //var oddNumber = numbers.Where(x => x % 2 != 0).ToList();

            //var numAccending = numbers.OrderBy(numbers => numbers).ToList();
            //var numAccending1 = numbers.Order().ToList();

            //var numDescending = numbers.OrderByDescending(numbers => numbers).ToList();
            //var numDescending2 = numbers.OrderDescending().ToList();

            //var checkEven = numbers.Any(x => x % 2 == 0).ToString();
            //var checkEven2 = numbers2.All(x => x % 2 == 0);

            ////skip and take

            //var subList = numbers.Take(3).ToList();
            //var sublist2 = numbers.TakeLast(3).ToList();
            //var sublist3 = numbers3.TakeWhile(x => x % 3 == 0).ToList();

            //var sublist4 = numbers.Skip(3).ToList();
            //var sublist5 = numbers.SkipLast(3).ToList();
            //var sublist6 = numbers3.SkipWhile(x => x % 3 == 0).ToList();


            //zip 

            //var num = new List<int>() { 1, 2 , 3 };
            //var words = new List<string>() { "One", "Two", "Three" };

            //var zipResult = words.Zip(num, (num, letter) => $"{num} is -- {letter}");


            //Join 



            //var result = from
            //                customer in customers
            //             join
            //                order in orders
            //             on
            //                customer.Id equals order.CustomerId
            //             select new {CustomerName = customer.Name, OrderID = order.OrderId };

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}


            //var result2 = from
            //               customer in customers
            //             join
            //                order in orders
            //             on
            //                customer.Id equals order.CustomerId into grouped
            //             select new { CustomerName = customer.Name, OrderID = grouped };

            //foreach (var item in result2)
            //{
            //    Console.WriteLine(item.CustomerName);
            //    foreach (var i in item.OrderID)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}

            // inner join with Method Syntax

            //var innerJoin = customers.Join(orders, customer => customer.Id, order => order.CustomerId, (customer, order) =>  new
            //{
            //        Name = customer.Name,
            //        OrderId = order.OrderId

            //});

            //foreach (var item in innerJoin)
            //{
            //    Console.WriteLine($"Name is: {item.Name} and Id is: {item.OrderId}");
            //}

            //Group join in Linq
            //var groupJoin = customers.GroupJoin(orders, c => c.Id, o => o.CustomerId, (c, grouped) => new { Name = c.Name, OrderID = grouped });

            //foreach(var item in groupJoin)
            //{
            //    Console.WriteLine($"Name is: {item.Name}");
            //    foreach(var i in item.OrderID)
            //    {
            //        Console.WriteLine($"Order is: {0}",i);
            //    }

            //}

            return View();
        }
    }
}
