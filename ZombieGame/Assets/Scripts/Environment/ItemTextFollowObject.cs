using System;
using Global;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Environment
{
    public class ItemTextFollowObject : TextFollowObject
    {
        public int index;
        
        private new void Update()
        {
            if (!GlobalShop.IsItemAvailable(index))
            {
                gameObject.SetActive(false);
            }
            
            //Do base update
            base.Update();
        }
        private void OnEnable()
        {
            SetText("$" + GlobalShop.Items[index].GetCost());
        }

        protected override void SetOffset()
        {
            offset = new Vector2(0, -0.25f);
        }

        private void SetText(string s)
        {
            UpdateText(s);
        }
    }
}
