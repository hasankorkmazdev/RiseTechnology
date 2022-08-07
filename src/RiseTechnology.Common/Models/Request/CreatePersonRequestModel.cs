namespace RiseTechnology.Common.Models.Request
{
    public class CreatePersonRequestModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
    }
}
