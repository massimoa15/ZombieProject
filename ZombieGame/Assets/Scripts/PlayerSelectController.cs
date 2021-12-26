using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelectController : MonoBehaviour
{
    private PlayerInputManager mgr;

    private UnityEvent joinEvent;

    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        mgr = GetComponent<PlayerInputManager>();
        joinEvent = new UnityEvent();
        
        //joinEvent.AddListener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
}
