
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VcksCore.DAL.Entities
{
    public class Participant
    {
        public int? ProfileId { get; set; }
        public VcksUserProfile Profile { get; set; }
    }
}
