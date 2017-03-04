using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VcksCore.DAL.Entities
{
    public class WallPost
    {
        public int Id { get; set; }
        public int? FromId { get; set; }
        public int? OwnerId { get; set; }
        public virtual VcksUserProfile From { get; set; }
        public virtual VcksUserProfile Owner { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
