using interval_recall.BLL.DTOs;
using interval_recall.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace interval_recall.API.Controllers
{
    [ApiController]
    [Route("api/v1/question-groups")]
    public class GroupController : ControllerBase
    {
        private readonly IQuestionGroupService _questionGroupService;

        private readonly ILogger<GroupController> _logger;

        public GroupController(ILogger<GroupController> logger, IQuestionGroupService questionGroupService)
        {
            _logger = logger;
            _questionGroupService = questionGroupService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(InQuestionGroupDTO questionGroupDTO)
        {
            try
            {
                await _questionGroupService.CreateAsync(new InQuestionGroupDTO()
                {
                    Title = questionGroupDTO.Title,
                    IntervalModifier = questionGroupDTO.IntervalModifier,
                    EasyBonus = questionGroupDTO.EasyBonus,
                    NewInterval = questionGroupDTO.NewInterval,
                    UserId = questionGroupDTO.UserId
                });
                return Ok();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _questionGroupService.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}