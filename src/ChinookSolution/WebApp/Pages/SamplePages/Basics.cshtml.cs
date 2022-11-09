#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        //  bacically this is an object, treat it as such

        //  data fields
        public string MyName { get; set; }

        //  properties
        //  The annotation BIndProperty ties a property in a PageModel class
        //      directly to a control in the Content Page.
        //  Data is transferred between the two automatically
        //  On the Content page, the control to use this property isl have a
        //      helper-tag called asp-for
        [BindProperty]
        public int? ID { get; set; }

        //  The annotation [TempData] stores data until it's read in another
        //      immediate request
        //  This annotation attribute has two method called Keep(string) and 
        //      Peek(string)  (used on Content page)
        //  Keep in a dictionary (name/value pair)
        //  Useful to redirect when data is required for more than a single request
        //  Implemented by TempData providers using either cookies or session state
        //  TempData is NOT bound to any particular control like BindProperty
        [TempData]
        public string FeedBack { get; set; }

        //  constructors

        //  behaviours (aka methods)
        public void OnGet()
        {
            //  Executes in response to a Get Request from the browser
            //  When the page is "first" accessed, the browser issues a Get Request
            //  When the page is refreshed WITHOUT a post request, the browser issues a
            //      Get request.
            //  When the page is retrieved in response to a form's POST using RedirectToPage()
            //  IF NOT RedirectToPage() is used on the POST, there is NO Get requested issued.

            Random rnd = new Random();
            int oddEven = rnd.Next(0, 25);
            if (oddEven % 2 == 0)
            {
                MyName = $"James is even {oddEven}";
            }
            else
            {
                MyName = null;
            }
        }

        //  Processing in response to a request from a form on a web page
        //  This request is referred to as a Post (method="post")

        //  General Post
        //  A general post occurs when a asp-page-handler is NOT used.
        //  The return datatype can be void, however, your will normally
        //      encounter the datatype IActionResult
        //  The IActionResult requires some type of request action
        //      on the return statement of the method OnPost()
        //  Typical actions:
        //  Page()
        //      :   does NOT issue a OnGet request
        //      :   remains on the current page
        //      :   a good action for form processing involving validation
        //              and with the catch of a try/catch
        //  ReDirectToPage()
        //      :   DOES issue a OnGet request
        //      :   Is used to retaining input values via the @Page and your
        //              BindProperty form controls on your form on the Content page

        public IActionResult OnPost()
        {
            //  This line of code is used to cause a delay in processing so
            //      we can see on the Network Activity some type of
            //      simulated processing
            Thread.Sleep(2000);

            //  Retrieve data via the Request object
            //  Request: web page to server
            //  Response: server to web page
            string buttonValue = Request.Form["theButton"];
            FeedBack = $"Button press is {buttonValue} with numeric input of {ID}";
            //return Page();  //  does not issue a OnGet()
            return RedirectToPage(new {ID=ID});  //  request for OnGet
        }






    }
}
