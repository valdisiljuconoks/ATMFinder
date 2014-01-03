using System.Collections.Generic;

namespace Viiar.AtmFinder.Contracts
{
    public class EntityIdComparer : IEqualityComparer<Entity>
    {
        public bool Equals(Entity x, Entity y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Entity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
