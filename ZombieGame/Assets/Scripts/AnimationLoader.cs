using Global;
using UnityEngine;

public class AnimationLoader : MonoBehaviour
{
    public RuntimeAnimatorController[] shopAnimations;

    void Start()
    {
        //Add the list of animations to the global class
        GlobalShop.Animations = shopAnimations;
    }
}
