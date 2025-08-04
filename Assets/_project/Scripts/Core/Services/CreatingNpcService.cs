using MyGame.Core.Npc;

namespace MyGame.Core
{
    public class CreatingNpcService
    {
        private NpcDatabase _database;
        public CreatingNpcService(NpcDatabase database)
        {
            _database = database; 
        }
   
        public NpcContext GetRandomNpc()
        {
            NpcContext context = new NpcContext(_database.GetRandomCustomer(), 1, 100);
            return context;
        }

        public Dialogue GetRandomDialogue()
        {
            return _database.GenerateRandomDialog();
        }
    }
}