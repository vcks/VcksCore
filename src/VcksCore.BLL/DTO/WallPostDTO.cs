using System;

namespace VcksCore.BLL.DTO
{
    public class WallPostDTO
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public virtual UserProfileDTO Owner { get; set; }
        public virtual UserProfileDTO From { get; set; }
        public int? FromId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }        
    }
}
