using interval_recall.BLL.Interfaces;
using interval_recall.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace interval_recall.API.Controllers
{
    [Controller]
    [Route("/api/v1/statistic")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionGroupStatistic>>> GetStatisticAsync(Guid? questionGroupId)
        {
            try
            {
                return await _statisticService.GetStatisticAsync(questionGroupId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveStatisticAsync(Guid questionGroupId)
        {
            try
            {
                await _statisticService.RemoveStatisticAsync(questionGroupId);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
