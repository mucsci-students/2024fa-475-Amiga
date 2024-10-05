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
        bool mouseDown = Input.GetMouseButton (0);
        bool spaceDown = Input.GetKey (KeyCode.Space);

        // Speed should be related to the curent movement speed of the enemy
        //  1 <= Speed <= 2 for realistic animations
        goblin.SetFloat ("Speed", 1 + xInput);
        manbat.SetFloat ("Speed", 1 + xInput);
        troll.SetFloat ("Speed", 1 + xInput);
        darkElf.SetFloat ("Speed", 1 + xInput);
        wizard.SetFloat ("Speed", 1 + xInput);

        // Is Attacking should be true while the enemy is attacking
        goblin.SetBool ("Is Attacking", mouseDown);
        troll.SetBool ("Is Attacking", mouseDown);
        wizard.SetBool ("Is Attacking", mouseDown);

        // Is Charging should be true while the enemy is charging its attack
        darkElf.SetBool ("Is Charging", mouseDown);

        // Is Jumping should be true while the enemy is in the air
        wizard.SetBool ("Is Jumping", spaceDown);
    }
}
