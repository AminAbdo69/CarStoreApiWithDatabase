namespace CarStoreApi.DTO
{
    public class UserLoginDTO
    {
        public required string role {  get; set; }
        public required string name { get; set; }
        public  string password { get; set; }
        public bool IsActive { get; set; }
    }
}
