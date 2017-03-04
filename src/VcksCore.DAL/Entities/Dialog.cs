using System.Collections.Generic;

namespace VcksCore.DAL.Entities
{
    public class Dialog
    {
        public int Id { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Dialog()
        {
            Messages = new List<Message>();
            Participants = new List<Participant>();
        }
    }
}
