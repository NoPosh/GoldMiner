
using System.Collections.Generic;

namespace MyGame.Core
{
    public class SpawnSettings
    {
        public List<SpawnEntry> Entires { get; } = new();
        public int MaxObjects { get; set; }
        public float SpawnInterval { get; set; }
    }
}
