namespace Domain.Events
{
    class ProficiencyCreatedEvent : BaseEvent
    {
        public Proficiency Proficiency { get; init; }
        public ProficiencyCreatedEvent(Proficiency proficiency)
        {
            Proficiency = proficiency;
        }
    }
}
