using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationLoader : MonoBehaviour
{
    public AnimatorController[] shopAnimations;

    void Start()
    {
        //Add the list of animations to the global class
        GlobalShop.Animations = shopAnimations;
    }
}
