using ProFSB.Domain.Common.Interfaces;

namespace Core.Entities.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public int? CreatedBy { get; set; }
    
    public DateTime? CreatedDate { get; set; }
    
    public int? UpdatedBy { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
    
    public bool IsDeleted { get; set; } = false;
}