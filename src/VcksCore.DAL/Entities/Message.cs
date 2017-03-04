using System;

using System.ComponentModel.DataAnnotations;

namespace VcksCore.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }

        [Required]
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }
    }
}
