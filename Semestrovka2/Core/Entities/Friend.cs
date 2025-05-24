using ProFSB.Domain.Common.Interfaces;

namespace Core.Entities
{
    public class Friend : IAuditableEntity
    {
        public Guid User1 { get; set; } = new();
        public Guid User2 { get; set; } = new();
        
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}