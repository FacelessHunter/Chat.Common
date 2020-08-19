using Common.Domain.Enums;

namespace Common.Domain.DTOs.ChatDTOs
{
    public class BaseChatDTO
    {
        public string Name { get; set; }

        public TypeOfChat Type { get; set; }
    }
}
