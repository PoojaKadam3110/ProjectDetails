using Azure;

namespace ProjectDetailsAPI.Common
{
    public abstract class Response : IResponse
    {
        public List<string> Errors { get; set; } = new();

        public bool IsSuccessful { get; set; }

    }
}
