using System;

using System.ComponentModel.DataAnnotations.Schema;

namespace VcksCore.DAL.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Guid FileId { get; set; }
        public File File { get; set; }
        public int? OwnerId { get; set; }
    }
}
