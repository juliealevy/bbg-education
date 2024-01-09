namespace BbgEducation.Api.Hal;
public interface IBbgResponseBuilder<TResult, TResponse> where TResponse : HalResponse
{
    TResponse? Build(TResult resultData, HttpContext context, bool selfFromPath,
        bool addGetLinks, bool addCreateUpdateLinks);
   
}