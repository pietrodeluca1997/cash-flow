using CF.CustomMediator.Models;

namespace CF.Core.DomainObjects.Abstracts
{
    public abstract class Entity
    {
        public Guid UniqueIdentifier { get; set; }
        public int Id { get; set; }

        protected Entity()
        {

        }

        #region Event/EventSourcing

        private List<Event> _events;
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        public void AddEvent(Event @event)
        {
            _events ??= new List<Event>();
            _events.Add(@event);
        }

        public void RemoveEvent(Event @event)
        {
            _events?.Remove(@event);
        }

        public void ClearEventHistory()
        {
            _events?.Clear();
        }

        #endregion

        #region Comparisons

        public override bool Equals(object? obj)
        {
            Entity? entityToCompare = obj as Entity;

            if (ReferenceEquals(this, entityToCompare)) return true;
            if (entityToCompare is null) return false;

            return UniqueIdentifier.Equals(entityToCompare.UniqueIdentifier) || Id.Equals(entityToCompare.Id);
        }

        public static bool operator ==(Entity first, Entity second)
        {
            if (first is null && second is null) return true;
            if (first is null || second is null) return false;

            return first.Equals(second);
        }

        public static bool operator !=(Entity first, Entity second)
        {
            return (first != second);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + UniqueIdentifier.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={UniqueIdentifier}]";
        }

        #endregion
    }
}