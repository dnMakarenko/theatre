using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Theatre.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public Guid UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Guid.Parse(User.Claims.First(c => c.Type == "UserId")?.Value);
                }

                return Guid.Empty;
            }
        }
    }
}
