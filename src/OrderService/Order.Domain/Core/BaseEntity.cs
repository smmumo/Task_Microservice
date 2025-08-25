using System;

namespace Order.Domain.Core
{
    /// <summary>
    /// Represents the base class that all entities derive from.
    /// </summary>
    public abstract class BaseEntity :  IEquatable<BaseEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        // protected BaseEntity(Guid id)
        //     : this()
        // {
        //     //Ensure.NotEmpty(id, "The identifier is required.", nameof(id));

        //     //Id = id;
        // }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        protected BaseEntity()
        {
        }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        //public Guid Id { get; private set; }

        public static bool operator == (BaseEntity a, BaseEntity b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator != (BaseEntity a, BaseEntity b) => !(a == b);

        /// <inheritdoc />
        public bool Equals(BaseEntity other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other); //|| Id == other.Id;
        }

        /// <inheritdoc />
        // public override bool Equals(object obj)
        // {
        //     if (obj is null)
        //     {
        //         return false;
        //     }

        //     if (ReferenceEquals(this, obj))
        //     {
        //         return true;
        //     }

        //     if (obj.GetType() != GetType())
        //     {
        //         return false;
        //     }

        //     if (!(obj is BaseEntity other))
        //     {
        //         return false;
        //     }

        //     if (Id == Guid.Empty || other.Id == Guid.Empty)
        //     {
        //         return false;
        //     }

        //     return Id == other.Id;
        // }


        /// <inheritdoc />
        //public override int GetHashCode() => Id.GetHashCode() * 41;

    /*
        private readonly List<IDomainEvent> _domainEvents = [];

        /// <summary>
        /// Gets the domain events. This collection is readonly.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Clears all the domain events from the <see cref="AggregateRoot"/>.
        /// </summary>
        public void ClearDomainEvents() => _domainEvents.Clear();
        
        /// <summary>
        /// Adds the specified <see cref="IDomainEvent"/> to the <see cref="AggregateRoot"/>.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

       */
    }
}
