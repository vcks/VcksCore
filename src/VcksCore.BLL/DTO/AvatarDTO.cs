using System;

namespace VcksCore.BLL.DTO
{
    public class AvatarDTO
    {
        public bool Default { get; set; }
        public Guid OriginalId { get; set; }
        public Guid SquareId { get; set; }
        public Guid Square_100Id { get; set; }
        public Guid Square_300Id { get; set; }
        public Guid Square_600Id { get; set; }
    }
}
