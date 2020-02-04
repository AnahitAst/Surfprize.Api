using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Surfprize.DAL;
using Surfprize.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surfprize.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        protected ApiContext ApiContext { get; set; }


        public BaseController(IServiceScopeFactory scopeFactory,
           IUnitOfWork unitOfWork,
           IHttpContextAccessor httpContextAccessor)
        {
            mapper = scopeFactory.CreateScope().ServiceProvider.GetService<IMapper>();
            this.unitOfWork = unitOfWork;
            ApiContext = new ApiContext(httpContextAccessor.HttpContext);
        }

        protected void Save() => unitOfWork.SaveChanges();

        protected async Task SaveAsync() => await unitOfWork.SaveChangesAsync();

        protected OkObjectResult ApiResult<TModel>(TModel model = default(TModel)) => new OkObjectResult(new ApiResponseModel<TModel>(model));

        protected OkObjectResult ApiResult<TModel>() => ApiResult<object>();

        protected OkObjectResult Unauthorized(string model = null) => new OkObjectResult(new UnauthorizedResponseModel(model));

        protected OkObjectResult Error(ErrorResponseModel model) => new OkObjectResult(model);

        protected OkObjectResult Error(ModelStateDictionary model)
        {
            List<Error> translatedErrors = new List<Error>();
            model.ToList()
                .ForEach(kvp =>
                translatedErrors.AddRange(kvp.Value.Errors.Select(me =>
                new Error("Error", me.ErrorMessage))));

            return Error(new ErrorResponseModel(translatedErrors));
        }

        protected OkObjectResult Error(string key, string message) => Error(new ErrorResponseModel(key, message));

        protected OkObjectResult Error() => Error(ControllerContext.ModelState);

        protected OkObjectResult HandleException(Exception e) => Error("Exception", e.Message);
    }

    public class ApiContext
    {
        private readonly HttpContext context;

        public ApiContext(HttpContext context)
        {
            this.context = context;
        }

        public bool IsAuthenticated => context.User.Identity.IsAuthenticated;
        public int UserId => int.Parse(context.User.Claims.Single(x => x.Type == "UserId").Value);
        public string UserEmail => context.User.Claims.Single(x => x.Type == "Email").Value;
        public HttpContext HttpContext => context;
    }
}
