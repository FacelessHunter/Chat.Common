namespace Common.Domain.DTOs.MessageDTOs
{
    public class ViewMessageDTO : BaseMessageDTO
    {
        public long Id { get; set; }

        public string Sender { get; set; }

        public long LastUpdatedTime { get; set; }

    }
}
