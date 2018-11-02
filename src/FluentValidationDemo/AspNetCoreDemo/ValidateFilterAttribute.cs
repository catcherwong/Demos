namespace AspNetCoreDemo
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class ValidateFilterAttribute : ResultFilterAttribute
    {    
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            //model valid not pass
            if(!context.ModelState.IsValid)
            {
                var entry = context.ModelState.Values.FirstOrDefault();

                var message = entry.Errors.FirstOrDefault().ErrorMessage;

                //modify the result
                context.Result = new OkObjectResult(new 
                { 
                    code = -1,
                    data = new JObject(),
                    msg= message,
                });
            }
        }
    }
}
