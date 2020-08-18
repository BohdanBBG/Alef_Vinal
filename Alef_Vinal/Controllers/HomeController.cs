using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Alef_Vinal.Models;
using Alef_Vinal.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Alef_Vinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataRepository _dataRepository;

        public HomeController(ILogger<HomeController> logger, IDataRepository dataRepository)
        {
            _logger = logger;
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetOne")]
        public async Task<IActionResult> GetOne([FromQuery] string id)
        {
            var codeEntity = await _dataRepository.GetOne(id);

            if (codeEntity != null)
            {
                return Ok(codeEntity);
            }

            return NotFound();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var codeEntities = await _dataRepository.GetAll();

            if (codeEntities != null)
            {
                return Ok(codeEntities);
            }

            return NotFound();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] NewCodeEntityDto codeEntity)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<NewCodeEntityDto, CodeEntity>()));

            await _dataRepository.Add(mapper.Map<CodeEntity>(codeEntity));

            return Ok();
        }

        [HttpPatch("Update")] 
        public async Task<IActionResult> Update([FromBody] CodeEntity codeEntity)
        {
            if (await _dataRepository.Update(codeEntity))
            {
                ControllerContext.HttpContext.Response.Cookies.Append("CodeEntity_Id", codeEntity.Id);

                return Ok();
            }

            return NotFound();

        }

    }
}
