using System;

namespace VcksCore.BLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }

        public int FromId { get; set; }

        public int ToId { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public bool Read { get; set; }
    }
}
