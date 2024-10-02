using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{

    [SerializeField] private Animator goblin;
    [SerializeField] private Animator manbat;
    [SerializeField] private Animator troll;
    [SerializeField] private Animator darkElf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Is Attacking will be set to false once the animation has ended
        goblin.SetBool ("Is Attacking", Input.GetMouseButtonDown (0));
        troll.SetBool ("Is Attacking", Input.GetMouseButtonDown (0));

        // Is Charging will not reset to false automatically, it must be done via script
        darkElf.SetBool ("Is Charging", Input.GetMouseButton (0));

        //  1 <= Speed <= 2 for realistic animations
        goblin.SetFloat ("Speed", 1 + Mathf.Abs (Input.GetAxis ("Horizontal")));
        manbat.SetFloat ("Speed", 1 + Mathf.Abs (Input.GetAxis ("Horizontal")));
        troll.SetFloat ("Speed", 1 + Mathf.Abs (Input.GetAxis ("Horizontal")));
        darkElf.SetFloat ("Speed", 1 + Mathf.Abs (Input.GetAxis ("Horizontal")));
    }
}
