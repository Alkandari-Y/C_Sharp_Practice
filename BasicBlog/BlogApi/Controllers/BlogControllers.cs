using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("/api/blogs")]
    [ApiController]
    public class BlogControllers : ControllerBase
    {
        
        public BlogControllers()
        {
            
        }
    }
}