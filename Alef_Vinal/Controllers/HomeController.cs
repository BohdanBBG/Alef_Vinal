using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Alef_Vinal.Models;
using Alef_Vinal.Repositories;

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
        public async Task<IActionResult> Add([FromBody] NewCodeEntity codeEntity)
        {
            await _dataRepository.Add(codeEntity);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CodeEntity codeEntity)
        {

            if (await _dataRepository.Update(codeEntity))
            {
                return Ok();
            }

            return NotFound();
        }

    }
}
