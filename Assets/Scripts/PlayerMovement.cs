using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public int potionModAmount = 0;

    public AudioClip jumpClip; // variable that takes audio data

    private float potionTimeMax = 10f;
    private float potionTimeCur = 0f; 

    float horizontalMove = 0f; // current movement is 0
    bool jumpFlag = false; // are we jumping, already jumped
    bool jump = false; // jump or no

    // Update is called once per frame
    void Update() // check for collision, keyboard press, etc
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //if the player inputs horizontal movement, multiply 25

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag) // if we already jumped turn off jumpFlag
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump")) // pressed jump, than change it to true
        {
            if (animator.GetBool("IsJumping") == false)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }
    }
    public void OnLanding() 
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate() // makes sure that it is called at steady interval
    {
        if (hasJumpPotion && potionTimeCur < potionTimeMax)
        {
            controller.m_JumpForceMod = potionModAmount;
            potionTimeCur += Time.fixedDeltaTime;
        }
        else
        {
            potionTimeCur = 0f;
            controller.m_JumpForceMod = 0;
            hasJumpPotion = false;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); //false for crouching
        
        if (jump)
        {
            jumpFlag = true;
        }
    }
}
