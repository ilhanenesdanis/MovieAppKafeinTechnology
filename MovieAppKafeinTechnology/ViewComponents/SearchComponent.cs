using Microsoft.AspNetCore.Mvc;

namespace MovieAppKafeinTechnology.ViewComponents
{
    public class SearchComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
