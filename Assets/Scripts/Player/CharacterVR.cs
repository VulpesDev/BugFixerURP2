using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterVR : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    float baseWalkingSpeed;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeedBase;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool canLook = true;

    int jumps = 0;

    float mass = 3.0f;
    Vector3 impact = Vector3.zero;
    float dashForce = 200f;

    Vector3 slideImpact = Vector3.zero;
    float slideForce = 75f;

    float baseHeight;
    bool sliding = false;

    bool canDash;
    [HideInInspector]public int dashes;
    public int maxDashes;
    [HideInInspector]public float dashTimer;
    public float cooldownTimeDash = 1f;

    [SerializeField]PostProcessManagement ppManager;

    FP_Shoot shootS;
    FP_ShootT shootST;
    Animator pistolAnime;

    [SerializeField] GameObject tommy, pistol;

    void Start()
    {
        StartCoroutine(DashCooldown());

        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lookSpeed = SeneManagement.sensitivity * 2.0f;

        lookSpeedBase = lookSpeed;

        baseHeight = characterController.height;

        baseWalkingSpeed = walkingSpeed;

        shootS = Camera.main.GetComponent<FP_Shoot>();
        shootST = Camera.main.GetComponent<FP_ShootT>();

    }

    Vector3 forward, right;

    void Update()
    {
        pistolAnime = Camera.main.GetComponent<FP_Shoot>().enabled ? shootS.pistolAnime :
            shootST.gunAnime;
        if (characterController.velocity.magnitude > 0 && characterController.isGrounded && !sliding)
            pistolAnime.SetBool("IsMoving", true);
        else pistolAnime.SetBool("IsMoving", false);


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShitchWeapons(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShitchWeapons(false);
        }

        DashUpdate();

        SlideUpdate();

        forward = transform.TransformDirection(Vector3.forward);
        right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? walkingSpeed * Input.GetAxisRaw("Vertical") : 0;
        float curSpeedY = canMove ? walkingSpeed * Input.GetAxisRaw("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButtonDown("Jump") && canMove && (characterController.isGrounded || jumps < 1))
        {
            moveDirection.y = jumpSpeed;
            jumps++;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (canLook)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        if (jumps != 0 && characterController.isGrounded) jumps = 0;
    }
    private void FixedUpdate()
    {
        if(transform.position.y <= -100)
        {
            GetComponent<Flesh>().TakeDamage(100);
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.fixedDeltaTime;
        }
        
        characterController.Move(moveDirection * Time.fixedDeltaTime);
        ImpactFixedUpdate();
        SlideImpactFixedUpdate();
    }

    void ShitchWeapons(bool rifle)
    {
        if (pistolAnime.GetCurrentAnimatorStateInfo(0).IsName("Reload")) return;
        pistol.SetActive(!rifle);
        tommy.SetActive(rifle);
        shootS.enabled = !rifle;
        shootST.enabled = rifle;
    }

    bool cooldown = false;

    void Slide()
    {
        
        SlideAddImpact(forward, (Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z))
            * slideForce / walkingSpeed * 0.8f);
        cooldown = true;

    }

    void SlideUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) { sliding = true; characterController.height = baseHeight / 4; }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            characterController.height = baseHeight;
            cooldown = false;
            sliding = false;
        }


        if (!cooldown && sliding && characterController.isGrounded)
        {
            Slide();
        }

        if (sliding) { if(characterController.isGrounded) walkingSpeed = baseWalkingSpeed / 3;  }
        else walkingSpeed = baseWalkingSpeed;

    }

    /// Dash
    void DashUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashes > 0)
        {
            MusicManager.DashSound();
            ppManager.StartDash();

            AddImpact((Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right).normalized, dashForce);
            dashes--;
            if(canDash)
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator DashCooldown()
    {
        canDash = false;
        while (dashes < maxDashes)
        {
            dashTimer = 0;
            for (int i = 0; i < cooldownTimeDash * 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                dashTimer += 0.01f / cooldownTimeDash;
            }
            dashes++;
        }
        canDash = true;
    }


    void ImpactFixedUpdate()
    {
        if (impact.magnitude > 0.2) characterController.Move(impact * Time.fixedDeltaTime);
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.fixedDeltaTime);
    }

    void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force / mass;
    }

    void SlideImpactFixedUpdate()
    {
        if (!sliding) slideImpact = Vector3.zero;
        if (slideImpact.magnitude > 0.2) characterController.Move(slideImpact * Time.fixedDeltaTime);
        slideImpact = Vector3.Lerp(slideImpact, Vector3.zero, 1 * Time.fixedDeltaTime);
    }

    void SlideAddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        slideImpact += dir.normalized * force / mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("JumpPad"))
        {
            moveDirection.y = jumpSpeed * 3;
            MusicManager.Spring();
            ppManager.StartDash();
        }
    }
}