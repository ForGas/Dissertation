using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities;

///<summary>Информация по восстановию</summary>
public class ResponseToolInfo : BaseModel
{
    public string Content { get; set; } = null!;
}
