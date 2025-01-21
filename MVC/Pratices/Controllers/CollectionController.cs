using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Pratices.Controllers
{
    public class CollectionController: Controller
    {
        public IActionResult Index()
        {
            //AarryList
            //var arryList = new ArrayList() { 1,"jhon", true, 45.6, 344,34,34,true, false, 9.55 };
            //int i = 0;

            //int number = (int)(double) arryList[3];


            //arryList.Insert(4, "Bob");

            //Console.WriteLine(arryList.Capacity);

            //foreach (var item in arryList)
            //{ 
            //    Console.WriteLine($"Element No {i} is :{item}");
            //    i++;
            //}

            //List

            //var city = new string[] { "dhaka", "london", "khulna" };
            //var list = new List<string>() { "cicago", "khulna" };

            //list.AddRange(city);
            ////list.Remove(list[0]);
            ////list.Remove("dhaka");
            //list.RemoveAt(0);
            //list.Clear();

            //foreach (string item in list)
            //{
            //    Console.WriteLine(item);
            //}

           
            //SortedList

            var sortedList = new SortedList<int, string>
            {
                {1, "One" },
                {2, "Two" },
                {3, "Three"},
                {8, "Eight"},
                {7, "Seven"}
            };
            sortedList.Add(5,"");
            sortedList.Add(6, "One");
            sortedList[5] = "Five";

            foreach (var item in sortedList)
            {
                Console.WriteLine(item);
            }

            List<string> list = sortedList.Select(x => x.Value.ToString()).ToList();
            foreach( string item in list )
            {
                Console.WriteLine(item);
            }

            return View();
        }

    }
}
