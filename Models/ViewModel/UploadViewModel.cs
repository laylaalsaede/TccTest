using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TccTest.Models.ViewModel
{
    public class UploadViewModel
    {
        public IFormFile NewFile { get; set; }
    }
}
