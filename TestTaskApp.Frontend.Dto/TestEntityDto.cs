using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestTaskApp.Frontend.Dto
{
    class TestEntityDto
    {

        public string Name { get; set; }

        public string Description { get; set; }
     
        public int Priority { get; set; }

        public bool Done { get; set; }
    }
}
