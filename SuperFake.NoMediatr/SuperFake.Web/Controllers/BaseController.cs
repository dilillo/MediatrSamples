using Microsoft.AspNetCore.Mvc;
using SuperFake.Business;
using System.Threading.Tasks;

namespace SuperFake.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected async Task<bool> CallBusinessTaskSafe(Task task)
        {
            try
            {
                await task;

                return true;
            }
            catch (BusinessException bx)
            {
                ModelState.AddModelError("", bx.Message);
            }

            return false;
        }
    }
}
