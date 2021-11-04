using Entities;

namespace Interactables
{
    public class HealInteractable : Interactable
    {
        public override void Interact(MBPlayer player)
        {
            //Heal player
            player.Heal();
        }
    }
}
