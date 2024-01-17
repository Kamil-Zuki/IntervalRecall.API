using interval_recall.BLL.Interfaces;
using interval_recall.Models.DTOs;
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
                    AmountOfNew = questionGroupDTO.AmountOfNew,
                    AmountOfLearn = questionGroupDTO.AmountOfLearn,
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
        public async Task<ActionResult<List<OutQuestionGroupDTO>>> GetAllAsync()
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

        [HttpGet("{questionGroupId}")]
        public async Task<ActionResult<List<OutQuestionGroupDTO>>> GetByIdAsync([FromRoute] Guid questionGroupId)
        {
            try
            {
                return Ok(await _questionGroupService.GetByIdAsync(questionGroupId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAsync(OutQuestionGroupDTO updateQuestionGroupDTO)
        {
            try
            {
                await _questionGroupService.UpdateAsync(updateQuestionGroupDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}