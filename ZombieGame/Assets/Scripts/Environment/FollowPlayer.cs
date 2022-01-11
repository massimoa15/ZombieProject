using System;
using Global;
using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Script to have the camera follow the player
    /// </summary>
    public class FollowPlayer : MonoBehaviour
    {
        private float playAreaLowestX = -13f;
        private float playAreaHighestX = 13f;
        private float playAreaLowestY = -13f;
        private float playAreaHighestY = 13f;
        


        //Player that is being followed
        public GameObject player;

        private float xOffset = 0;
        private float yOffset = 0; 
        private float zOffset = -2;

        private float cameraHalfWidth;
        private float cameraHalfHeight;

        private Camera cam;
        
        private void Start()
        {
            cam = GetComponent<Camera>(); 
            cameraHalfWidth = cam.orthographicSize * cam.aspect;
            cameraHalfHeight = cam.orthographicSize;
        }    
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
            //player object is not null so there is a player in the game for the camera to follow
            else
            {
                Vector3 playerPos = player.transform.position;
                
                Vector3 newPos = Vector3.zero;
                //Set z value since it's always gonna be the same
                newPos.z = playerPos.z + zOffset;
                //Only update the x if the player is not too close to the wall
                if (playerPos.x < cameraHalfWidth + playAreaLowestX)
                {
                    newPos.x = playAreaLowestX + cameraHalfWidth + xOffset;
                }
                else if (playerPos.x > playAreaHighestX - cameraHalfWidth)
                {
                    newPos.x = playAreaHighestX - cameraHalfWidth + xOffset;
                }
                else
                {
                    newPos.x = playerPos.x + xOffset;
                }
                //Only update the y if the player is not too close to the wall.
                if (playerPos.y < cameraHalfHeight + playAreaLowestY)
                {
                    newPos.y = playAreaLowestY + cameraHalfHeight + yOffset;
                }
                else if (playerPos.y > playAreaHighestY - cameraHalfHeight)
                {
                    newPos.y = playAreaHighestY - cameraHalfHeight + yOffset;
                }
                else
                {
                    newPos.y = playerPos.y + yOffset;
                }

                transform.position = newPos;
            }
        }
    }
}
