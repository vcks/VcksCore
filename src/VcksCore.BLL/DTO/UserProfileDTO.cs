namespace VcksCore.BLL.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public bool Deleted { get; set; }
        public AvatarDTO Avatar { get; set; }
    }
}
