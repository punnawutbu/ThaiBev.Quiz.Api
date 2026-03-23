using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThaiBev.Quiz.Api.Shared.Models;
using ThaiBev.Quiz.Api.Shared.Services;

namespace ThaiBev.Quiz.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuestion()
        {
            var result = await _questionService.GetAllQuestionAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionReq request)
        {
            try
            {
                var result = await _questionService.CreateQuestionAsync(request.Text, request.Choices);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var deleted = await _questionService.DeleteQuestionsAsync(id);

            if (!deleted)
                return NotFound(new { message = "Question not found." });

            return Ok();
        }
    }
}