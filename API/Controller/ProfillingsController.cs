using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfillingsController : BaseController<Profilling, ProfillingRepository,int>
    {
        private readonly ProfillingRepository profillingRepository;
        public ProfillingsController(ProfillingRepository profillingRepository) : base(profillingRepository)
        {
            this.profillingRepository = profillingRepository;
        }
    }
}
