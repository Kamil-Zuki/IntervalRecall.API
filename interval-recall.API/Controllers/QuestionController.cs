using interval_recall.BLL.Interfaces;
using interval_recall.Models.DTOs;
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
            try
            {
                var responce = await _learningService.RecallAsync(userResponces);
                return Ok(new SubmissionResult() { Correct = responce.Item1, Incorrect = responce.Item2});
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<OutRecallQuestionGroupDTO>>> GetRecallQuestions([FromQuery] Guid? questionGroupId)
        {
            try
            {
                return Ok(await _questionService.GetRecallQuestionsAsync(questionGroupId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
