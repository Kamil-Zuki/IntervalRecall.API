namespace interval_recall.BLL.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public Guid? UserGroupId { get; set; }
    }
}
