using System;
using UnityEngine;

namespace Environment
{
    public class UIFollowObject : MonoBehaviour
    {
        public GameObject following;
        public Vector3 offset;

        private Camera mainCam;
        
        private void Start()
        {
            mainCam = Camera.main;
        }

        private void Update()
        {
            SetPosition();
        }

        private void SetPosition()
        {
            if (following != null)
            {
                transform.position = mainCam.WorldToScreenPoint(following.transform.position) + offset;
            }
        }

        public void SetFollowingObject(GameObject obj)
        {
            following = obj;
        }
    }
}