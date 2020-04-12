using Microsoft.AspNetCore.Mvc;
using SuperFake.Domains;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected async Task<bool> ExecuteCommandSafe(Task task)
        {
            try
            {
                await task;

                return true;
            }
            catch (DomainException dx)
            {
                ModelState.AddModelError("", dx.Message);
            }

            return false;
        }
    }
}
