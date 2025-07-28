using System.Numerics;

namespace MyGame.Core
{
    public class SpawnEntry
    {
        public string ItemId { get; set; } //Id вместо ссылки на SO
        public float SpawnChance { get; set; }
        public UnityEngine.Vector2Int PotentialRange { get; set; }
        public float ArtifactChance { get; set; }
    }
}
