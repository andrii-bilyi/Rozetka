namespace Rozetka.Data.Entity
{
    public class LoginJournalItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Moment { get; set; }
    }
}
