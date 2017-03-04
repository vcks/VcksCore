using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using VcksCore.BLL.DTO;

namespace VcksCore.Models
{
    public class DialogsPanelViewModel
    {
        public List<DialogViewModel> Dialogs;
        public  class DialogViewModel
        {
            public int DialogId;
            public int UserId;
            public string Avatar100;
            public string FullName;
            public string TrimmedBody;
            public int UnreadMessagesCount;
        }

        public DialogsPanelViewModel(List<DialogDTO> dialogs, int currentUserId)
        {
            Dialogs = new List<DialogViewModel>();
            foreach(var dialog in dialogs)
            {
                ParticipantDTO anotherUser = GetAnotherUser(dialog.Participants, currentUserId);
                string body = dialog.Messages.Count > 0 ? dialog.Messages.Last().Body : string.Empty;
                var dialogViewModel = new DialogViewModel()
                {
                    DialogId = dialog.Id,
                    UnreadMessagesCount = dialog.Messages.Where(m => !m.Read && m.ToId.Equals(currentUserId)).Count(),                    
                    UserId = anotherUser.ProfileId,
                    Avatar100 = $"/File/{anotherUser.Profile.Avatar.Square_100Id}",
                    FullName = $"{anotherUser.Profile.FirstName} {anotherUser.Profile.LastName}",
                    TrimmedBody = body.Length > 60 ? body.Substring(0, 60) : body
                };
                Dialogs.Add(dialogViewModel);
            }
        }
        public static ParticipantDTO GetAnotherUser(List<ParticipantDTO> participants, int currentUserId)
        {
            var f = participants.Where(p => p.ProfileId != currentUserId).ToList();
            return f.Count == 0 ? participants.Where(p => p.ProfileId == currentUserId).First() : f.First();
        }
    }
}