using System.ComponentModel;

namespace Models
{
    public abstract class Entity
    {
        [DisplayName("Identifier")]
        public int Id { get; set; }

    }
}