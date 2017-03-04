using System;

namespace VcksCore.BLL.DTO
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public int OwnerId { get; set; }
    }
}