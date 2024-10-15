using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{

    [SerializeField] private Animator goblin;
    [SerializeField] private Animator manbat;
    [SerializeField] private Animator troll;
    [SerializeField] private Animator darkElf;
    [SerializeField] private Animator wizard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Mathf.Abs (Input.GetAxis ("Horizontal"));
        bool mouse = Input.GetMouseButton (0);
        bool mouseDown = Input.GetMouseButtonDown (0);
        bool space = Input.GetKey (KeyCode.Space);

        // Speed should be related to the curent movement speed of the enemy
        //  1 <= Speed <= 2 for realistic animations
        goblin.SetFloat ("Speed", 1 + xInput);
        manbat.SetFloat ("Speed", 1 + xInput);
        troll.SetFloat ("Speed", 1 + xInput);
        darkElf.SetFloat ("Speed", 1 + xInput);
        wizard.SetFloat ("Speed", 1 + xInput);

        // Is Attacking should be true while the player is attacking
        wizard.SetBool ("Is Attacking", mouse);

        // Is Charging should be true while the enemy is charging its attack
        darkElf.SetBool ("Is Charging", mouse);

        // Is Jumping should be true while the enemy is in the air
        wizard.SetBool ("Is Jumping", space);

        if (mouseDown) 
        {
            // Attacking should be triggered whenever the enemy attacks
            goblin.SetTrigger ("Attack");
            troll.SetTrigger ("Attack");

            // Take Damage should be triggered whenever the enemy/player takes damage
            goblin.SetTrigger ("Take Damage");
            manbat.SetTrigger ("Take Damage");
            troll.SetTrigger ("Take Damage");
            darkElf.SetTrigger ("Take Damage");

            // Shield Damage should be triggered whenever the player's shield absorbs the damage
            wizard.SetTrigger ("Shield Damage");
        }
    }
}
