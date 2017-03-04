using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;

using VcksCore.BLL.Services;
using VcksCore.BLL.DTO;
using VcksCore.Models;

using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;

using Microsoft.AspNetCore.Http;

namespace VcksCore.Hubs
{
    [Authorize]
    [HubName("messagesHub")]
    public class MessagesHub : Hub
    {
        protected IHttpContextAccessor httpContextAccessor;
        protected int GetUserId() => int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
        public MessagesHub(IHttpContextAccessor httpContextAccessor, DialogService dialogService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dialogService = dialogService;
        }

        static List<User> Users = new List<User>();
        class User
        {
            public int Id;
            public int DialogId;
            public string ConnectionId;

            public User(string ConnectionId, int Id)
            {
                this.ConnectionId = ConnectionId;
                this.Id = Id;
            }
        }

        readonly DialogService dialogService;

        public async Task connect()
        {
            var id = Context.ConnectionId;

            if (!Users.Any(u => u.ConnectionId.Equals(id)))
            {
                User user = new User(id, GetUserId());
                Users.Add(user);
                await GetDialogsPanel(user);
            }
        }

        public async Task getDialog(int userId)
        {
            User currentUser = Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId && u.Id == GetUserId());
            if (currentUser != null)
            {
                var dialog = await dialogService.GetDialogBetweenUsers(new int[] { currentUser.Id, userId });
                if (dialog != null)
                {
                    currentUser.DialogId = dialog.Id;
                    Clients.Caller.refreshDialog(dialog.Messages);
                }
            }
        }

        public async Task markAsRead(int Id)
        {
            User currentUser = Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId && u.Id == GetUserId());
            DialogDTO dialog = await dialogService.GetById(currentUser.DialogId);
            await dialogService.MarkMessagesAsRead(dialog.Messages.Where(m => m.ToId == currentUser.Id && !m.Read && m.Id <= Id).ToList());
        }

        public async Task sendMessage(int userId, string text)
        {
            int currentUserId = GetUserId();
            DialogDTO dialog = await dialogService.GetDialogBetweenUsers(new int[] { currentUserId, userId });
            if (dialog != null && !string.IsNullOrEmpty(text))
            {
                var message = new MessageDTO()
                {
                    ToId = userId,
                    Body = text.Trim(),
                    FromId = currentUserId,
                    Date = DateTime.Now
                };
                await dialogService.WriteMessage(dialog.Id, message);

                dialog = await dialogService.GetDialogBetweenUsers(new int[] { currentUserId, userId });

                // обновляем окно диалога у тех кто его открыл
                var participantsForRedreshDialog = Users.Where(u => dialog.Participants.Any(p => p.ProfileId == u.Id) && u.DialogId == dialog.Id).Select(u => u.ConnectionId).ToList();
                Clients.Clients(participantsForRedreshDialog).refreshDialog(dialog.Messages);

                // обновляем список диалогов у отправителя и получателя
                var participantsForRedreshDialogs = Users.Where(u => dialog.Participants.Any(p => p.ProfileId == u.Id));
                foreach (User p in participantsForRedreshDialogs) await GetDialogsPanel(p);
            }
        }

        async Task GetDialogsPanel(User user)
        {
            var dialogs = await dialogService.GetByUserId(user.Id,20,0,true);
            DialogsPanelViewModel dp = new Models.DialogsPanelViewModel(dialogs, user.Id);
            Clients.Client(user.ConnectionId).refreshDialogs(dp);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
                Users.Remove(item);
            return base.OnDisconnected(stopCalled);
        }
    }
}