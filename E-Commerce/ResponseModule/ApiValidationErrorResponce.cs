namespace E_Commerce.ResponseModule
{
    public class ApiValidationErrorResponce : ApiException
    {
        public ApiValidationErrorResponce() : base(400)
        {

        }




        public IEnumerable<string> Errors { get; set; }
    }
    
}
