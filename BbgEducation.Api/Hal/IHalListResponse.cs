namespace BbgEducation.Api.Hal;

public interface IHalListResponse<TResponse> 
    where TResponse : HalResponse
    
{
    public List<TResponse> Items{ get; set; }
}
