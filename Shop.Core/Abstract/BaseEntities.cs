namespace Shop.Core.Abstract;


public abstract class BaseEntities
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime ModifiedTime { get; set; } = DateTime.Now;
    public virtual bool IsDeleted { get; set; }

}

