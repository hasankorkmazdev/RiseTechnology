namespace RiseTechnology.Contact.API.Models.Response
{
    public class PersonResponseModel:Base.BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
    }
}
