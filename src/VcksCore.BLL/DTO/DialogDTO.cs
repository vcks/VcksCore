using System.Collections.Generic;

namespace VcksCore.BLL.DTO
{
    public class DialogDTO
    {
        public int Id { get; set; }
        public List<ParticipantDTO> Participants { get; set; }
        public List<MessageDTO> Messages { get; set; }

        public DialogDTO()
        {
            Messages = new List<MessageDTO>();
        }
    }
}
