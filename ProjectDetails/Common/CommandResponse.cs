namespace ProjectDetailsAPI.Common
{
    public class CommandResponse : Response
    {
        public dynamic Data { get; set; }
        public CommandResponse() { }
        public CommandResponse(string Message, bool status = false)
        {
            IsSuccessful = status;
            Errors.Add(Message);
        }

        public CommandResponse(List<string> Messages, bool status = false)
        {
            IsSuccessful = status;
            Errors.AddRange(Messages);
        }
    }
}
