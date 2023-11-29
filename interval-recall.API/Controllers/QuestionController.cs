using interval_recall.BLL.DTOs;
using interval_recall.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace interval_recall.API.Controllers
{
    [Route("api/v1/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ILearningService _learningService;
        public QuestionController(IQuestionService questionService, ILearningService learningService)
        {
            _questionService = questionService;
            _learningService = learningService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(List<InQuestionDTO> questionDTOs)
        {
            try
            {
                await _questionService.CreateRangeAsync(questionDTOs);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("answers")]
        public async Task<ActionResult> GetAnswersAsync(List<InUserResponceDTO> userResponces)
        {
            await _learningService.Recall(userResponces);
            return Ok();
        }

        [HttpGet("recall-questions")]
        public async Task<IActionResult> GetRecallQuestions()
        {
            return Ok(await _questionService.GetRecallQuestions());
        }

    }
}
