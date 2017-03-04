using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;
using VcksCore.DAL.Repositories;
using VcksCore.BLL.DTO;
using VcksCore.BLL.Infrastructure;

namespace VcksCore.BLL.Services
{
    public class DialogService
    {
        readonly DialogManager dialogManager;
        readonly IMapper mapper;

        public DialogService(DialogManager dialogManager)
        {
            mapper = AutoMapperConfiguration.Get().CreateMapper();
            this.dialogManager = dialogManager;
        }

        public async Task<DialogDTO> GetDialogBetweenUsers(int[] users)
        {
            var dialog = await dialogManager.GetDialogBetweenUsers(users);
            var dialogDTO = mapper.Map<Dialog, DialogDTO>(dialog);
            return dialogDTO;
        }

        public async Task MarkMessagesAsRead(List<MessageDTO> messagesDTO)
        {
            var messages = mapper.Map<List<MessageDTO>,List<Message>>(messagesDTO);
            await dialogManager.MarkMessagesAsRead(messages);
        }

        public async Task<List<DialogDTO>> GetByUserId(int userId, int count, int offset, bool last)
        {
            var dialogs = await dialogManager.GetByUserId(userId, count, offset, last);
            var dialogsDTO = mapper.Map<List<Dialog>, List<DialogDTO>>(dialogs);
            return dialogsDTO;
        }

        public async Task<DialogDTO> GetById(int Id)
        {
            var dialog = await dialogManager.GetById(Id);
            var dialogDTO = mapper.Map<Dialog, DialogDTO>(dialog);
            return dialogDTO;
        }

        public async Task WriteMessage(int dialogId, MessageDTO messageDTO)
        {
            var message = mapper.Map<MessageDTO, Message>(messageDTO);
            await dialogManager.WriteMessage(dialogId, message);
        }
    }
}
