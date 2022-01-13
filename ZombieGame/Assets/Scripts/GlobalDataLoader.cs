using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalDataLoader : MonoBehaviour
{
    public RuntimeAnimatorController[] shopAnimations;

    public PlayerInputManager mgr;

    public List<GameObject> PlayerPrefabs;

    void Start()
    {
        //Add the list of animations to the global class
        GlobalShop.Animations = shopAnimations;
        GlobalData.mgr = mgr;
        GlobalData.PlayerPrefabs = PlayerPrefabs;
    }
}
