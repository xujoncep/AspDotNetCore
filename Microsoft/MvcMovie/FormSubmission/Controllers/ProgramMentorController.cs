using FormSubmission.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormSubmission.Controllers
{
    public class ProgramMentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        [HttpPost]
        public int Sub(int number1, int number2)
        {
            return number1 - number2;
        }

        [HttpPost]
        public Calculator Calculation(int number1, int number2)
        {
            Calculator calculator = new Calculator();

            calculator.Add = number1 + number2;
            calculator.Sub = number1 - number2;
            calculator.Mul = number1 * number2;
            //calculator.Div = (decimal)(number1 / number2);

            return calculator;
        }
    }
}
