

using System.Numerics;

namespace MyGame.Core.Npc
{
    public class NpcContext
    {
        public NpcData Data { get;}
        public Vector3 Position { get; set; }
        public bool IsActive { get; set; }
        public float Mood { get; set; }

        public void UpdateBehavior(float deltaTime) //Тут логика принятия решений
        {

        }
    }
}
