using System.Threading.Tasks;
using AutoMapper;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;
using VcksCore.DAL.Repositories;
using VcksCore.BLL.DTO;
using VcksCore.BLL.Infrastructure;

namespace VcksCore.BLL.Services
{
    public class FileService
    {
        readonly FileManager fileManager;
        readonly IMapper mapper;

        public FileService(FileManager fileManager)
        {
            mapper = AutoMapperConfiguration.Get().CreateMapper();
            this.fileManager = fileManager;
        }

        public async Task Create(FileDTO fileDTO)
        {
            var file = mapper.Map<FileDTO, File>(fileDTO);
            await fileManager.Create(file);
        }

        public async Task<FileDTO> GetById(System.Guid id)
        {
            var file = await fileManager.GetById(id);
            var fileDTO = mapper.Map<File, FileDTO>(file);
            return fileDTO;
        }

        public async Task Update(FileDTO fileDTO)
        {
            var file = mapper.Map<FileDTO, File>(fileDTO);
            await fileManager.Update(file);
        }     
    }
}
