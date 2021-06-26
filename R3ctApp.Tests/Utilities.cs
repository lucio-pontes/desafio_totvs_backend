using R3ctApp.Api.Models;
using R3ctApp.DataAccess;
using System.Collections.Generic;

namespace R3ctApp.Tests
{
    class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.HireCandidates.AddRange(GetCandidatos());
            db.HireJobs.AddRange(GetVagas());
            db.HireCurriculums.AddRange(GetCurriculos());
            db.HireProcess.AddRange(GetCandidaturas());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.HireCandidates.RemoveRange(db.HireCandidates);
            InitializeDbForTests(db);
        }

        public static List<HireCandidate> GetCandidatos()
        {
            return new List<HireCandidate>()
            {
                new HireCandidate(){
                    Id = 1,
                    Name = "Maria José",
                    Age = 23,
                    City = "Belo Horizonte",
                    Phone = "(31) 93457-4588",
                    Email = "mariajose@tempmail.com",
                    Status = "Active"
                },
                new HireCandidate(){
                    Id = 2,
                    Name = "João Batista",
                    Age = 24,
                    City = "Belo Horizonte",
                    Phone = "(31) 99276-7839",
                    Email = "joaobatista@tempmail.com",
                    Status = "Active"
                },
                new HireCandidate(){
                    Id = 3,
                    Name = "Raimundo Nonato (excluir)",
                    Age = 25,
                    City = "São Paulo",
                    Phone = "(11) 95555-4444",
                    Email = "raimundononato@tempmail.com",
                    Status = "Canceled"
                }
            };
        }

        public static List<HireJob> GetVagas()
        {
            return new List<HireJob>()
            {
                new HireJob(){
                    Id = 1,
                    Description = "Analista de Sistemas Fullstack",
                    Status = "Active"
                },
                new HireJob(){
                    Id = 2,
                    Description = "Analista de Negócios",
                    Status = "Process"
                },
                new HireJob(){
                    Id = 3,
                    Description = "Analista de Testes",
                    Status = "Active"
                }
            };
        }
        public static List<HireCurriculum> GetCurriculos()
        {
            return new List<HireCurriculum>()
            {
                new HireCurriculum(){
                    Id = 1,
                    HireCandidateId = 1,
                    WorksHistory = "",
                    AcademicsHistory = "",
                    Courses = "",
                    Summary = ""
                },
                new HireCurriculum(){
                    Id = 2,
                    HireCandidateId = 2,
                    WorksHistory = "",
                    AcademicsHistory = "",
                    Courses = "",
                    Summary = ""
                },
                new HireCurriculum(){
                    Id = 3,
                    HireCandidateId = 3,
                    WorksHistory = "",
                    AcademicsHistory = "",
                    Courses = "",
                    Summary = ""
                }
            };
        }
        public static List<HireProcess> GetCandidaturas()
        {
            return new List<HireProcess>()
            {
                new HireProcess(){
                    Id = 1,
                    HireCandidateId = 1,
                    HireJobId = 1,
                    Status = "Active"
                },
                new HireProcess(){
                    Id = 2,
                    HireCandidateId = 2,
                    HireJobId = 2,
                    Status = "Active"
                },
                new HireProcess(){
                    Id = 3,
                    HireCandidateId = 1,
                    HireJobId = 2,
                    Status = "Active"
                }
            };
        }
    }
}
