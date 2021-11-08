using Global;
using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Script to have the camera follow the player
    /// </summary>
    public class FollowPlayer : MonoBehaviour
    {
        //Player that is being followed
        public GameObject player;

        private float xOffset = 0;
        private float yOffset = 0; 
        private float zOffset = -2;
    
        void Update()
        {
            //If player object is not assigned, assign one
            if (player == null)
            {
                //Can't assign one if there are no players
                if (GlobalData.PlayerObjects.Count > 0)
                {
                    //Set the player as player 1
                    player = GlobalData.PlayerObjects[0];
                }
            }
            else
            {
                /*
                //Set the position of the camera to the position of the player, with offsets
                transform.position = new Vector3(player.transform.position.x + xOffset,
                    player.transform.position.y + yOffset, player.transform.position.z + zOffset);
                    */
                transform.position = player.transform.position + new Vector3(xOffset, yOffset, zOffset);
            }
        }
    }
}
