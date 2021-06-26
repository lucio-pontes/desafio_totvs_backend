using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace R3ctApp.Api.Responses
{
    public class HireCurriculumResp
    {

        public long Id { get; set; }

        public String Name { get; set; }
        
        public String WorksHistory { get; set; }

        public String AcademicsHistory { get; set; }

        public String Courses { get; set; }

        public String Summary { get; set; }

    }
}
