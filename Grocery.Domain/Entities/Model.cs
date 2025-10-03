using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Grocery.Domain.Entities
{
    /// <summary>
    /// Abstract base class for all domain entities in the application.
    /// Provides common properties and functionality that all entities share.
    /// Follows HBO-ICT coding guidelines for domain modeling and inheritance.
    /// </summary>
    /// <param name="id">The unique identifier for the entity</param>
    /// <param name="name">The name of the entity</param>
    public abstract partial class Model(int id, string name) : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// This property is required and must be unique within the entity type.
        /// </summary>
        [Required]
        public int Id { get; set; } = id;

        /// <summary>
        /// Gets or sets the name of this entity.
        /// This property is observable and will notify UI when changed.
        /// </summary>
        [ObservableProperty]
        private string _name = name;

        #endregion

        #region Override Methods

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// Two entities are considered equal if they have the same Id and type.
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the objects are equal; otherwise, false</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Model other && GetType() == other.GetType())
            {
                return Id == other.Id;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// The hash code is based on the Id property.
        /// </summary>
        /// <returns>A hash code for this instance</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this entity.
        /// Format: "EntityType: Name (Id: X)"
        /// </summary>
        /// <returns>A string representation of this entity</returns>
        public override string ToString()
        {
            return $"{GetType().Name}: {Name} (Id: {Id})";
        }

        #endregion
    }
}
