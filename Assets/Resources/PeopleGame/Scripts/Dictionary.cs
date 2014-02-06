using UnityEngine;
using System.Collections.Generic;

namespace Dictionary
{
    public class Prefabs
    {
        public enum type { Person };

        private static readonly IDictionary<type, string> prefabNames = new Dictionary<type, string>
        {
            {type.Person, "PeopleGame/Prefabs/Person"}
        };
        public static IDictionary<type, string> PREFAB_NAMES { get { return prefabNames; } }
    }

    public class Names
    {
        public enum firstNames { };
        public enum lastNames { };
    }
}
