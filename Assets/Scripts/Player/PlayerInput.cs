using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerControls controls;
    Player player;

    void Awake(){
        controls = new PlayerControls();
        player = GetComponent<Player>();
    }

    void OnEnable(){
        controls.Enable();
    }

    void OnDisable(){
        controls.Disable();
    }

    void Start()
    {
        controls.Player.Jump.performed += _ => player.Jump();
        controls.Player.Pickup.performed += _ => player.PickUpGun();
    }

    void Update(){
        player.direction = controls.Player.Move.ReadValue<float>();
    }
}
