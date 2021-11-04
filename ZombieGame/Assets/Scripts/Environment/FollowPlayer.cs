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
                if (GlobalData.PlayerObject != null)
                {
                    player = GlobalData.PlayerObject;
                }
            }
            else
            {
                //Set the position of the camera to the position of the player, with offsets
                transform.position = new Vector3(player.transform.position.x + xOffset,
                    player.transform.position.y + yOffset, player.transform.position.z + zOffset);
            }
        }
    }
}
