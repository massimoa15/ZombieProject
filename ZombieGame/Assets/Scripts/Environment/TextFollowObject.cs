using System;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    /// <summary>
    /// Will make the attached Text object follow the object this script is attached to
    /// </summary>
    public abstract class TextFollowObject : MonoBehaviour
    {
        public Text text;

        private Camera mainCam;

        protected Vector2 offset;

        protected void Start()
        {
            mainCam = Camera.main;
            SetOffset();
        }

        protected void Update()
        {
            SetPosition();
        }

        /// <summary>
        /// Update the text shown through this text object
        /// </summary>
        /// <param name="s"></param>
        public void UpdateText(String s)
        {
            text.text = s;
        }

        /// <summary>
        /// In classes that inherit this class, defines how much the text should be offset from the object
        /// </summary>
        protected abstract void SetOffset();

        protected void SetPosition()
        {
            Vector2 tempPos = transform.position;
            tempPos += offset;
            tempPos = mainCam.WorldToScreenPoint(tempPos);
            text.transform.position = tempPos;
        }
    }
}
