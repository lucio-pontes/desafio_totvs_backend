using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace R3ctApp.Api.Models
{
    [Table("hire_curriculums")]
    public class HireCurriculum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("hire_candidate_id")]
        public long HireCandidateId { get; set; }
        
        public HireCandidate HireCandidate { get; set; }

        [Column("works_history")]
        public String WorksHistory { get; set; }

        [Column("academics_history")]
        public String AcademicsHistory { get; set; }

        [Column("courses")]
        public String Courses { get; set; }

        [Column("summary")]
        public String Summary { get; set; }

    }
}
