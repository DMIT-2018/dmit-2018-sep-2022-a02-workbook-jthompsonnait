#nullable disable 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class ControlsModel : PageModel
    {
        [TempData]
        public string Feedback { get; set; }

        [BindProperty]
        public string EmailText { get; set; }
        [BindProperty]
        public string PasswordText { get; set; }

        [BindProperty] public DateTime DateText { get; set; } = DateTime.Now;
        [BindProperty] public TimeSpan TimeText { get; set; } = DateTime.Now.TimeOfDay;

        [BindProperty]
        public string Meal { get; set; }
        //  Assume this array is actually data retrieve from the database
        public string[] Meals { get; set; } = new[] { "breakfast", "lunch", "dinner", "snacks" }; 

        [BindProperty]
        public bool AcceptanceBox { get; set; }

        [BindProperty]
        public string MessageBody { get; set; }






        public void OnGet()
        {
        }

        public IActionResult OnPostTextBox()
        {
            Feedback = $"Email {EmailText}; Password {PasswordText}; Date {DateText}; Time {TimeText}";
            return Page();
        }

        public IActionResult OnPostRadioCheckArea()
        {
            Feedback = $"Meal {Meal}; Acceptance {AcceptanceBox}; Message {MessageBody}";
            return Page();
        }


    }
}
