using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.v1.Controllers.Articles.ArticleRequests;
using StoreWebApi.v1.Controllers.Articles.ArticleResponses;
using StoreWebApi.v1.Controllers.Articles.Services.Intefaces;
using System.Collections.Generic;

namespace StoreWebApi.v1.Controllers.Articles
{
    [Route("api/v1/articles")]
    [ApiController]
    public class ArticleController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ArticleResponse> Submit([FromBody] ArticleSubmitRequest articleRequest)
        {
            var result = _articleService.Submit(articleRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticleInfoResponse>> GetAll()
        {
            var result = _articleService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ArticleResponse> GetPurchase(int articleId)
        {
            var result = _articleService.Get(articleId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ArticleResponse> Remove(int articleId)
        {
            var result = _articleService.Remove(articleId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
