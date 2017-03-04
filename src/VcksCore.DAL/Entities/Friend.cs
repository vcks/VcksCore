 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VcksCore.DAL.Entities
{
    public class Friend
    {
        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual VcksUserProfile Profile { get; set; }
    }
}
