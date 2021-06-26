using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace R3ctApp.Api.Models
{
    [Table("hire_process")]
    public class HireProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("hire_candidate_id")]
        public long HireCandidateId { get; set; }
        
        public HireCandidate HireCandidate { get; set; }

        [Column("hire_job_id")]
        public long HireJobId { get; set; }
        
        public HireJob HireJob { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
