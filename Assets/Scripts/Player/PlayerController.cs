using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    public UIManager UI;
    CustomActions input;

    NavMeshAgent agent;
    Animator animator;

    //Interaction Components
    PlayerInteraction playerInteraction;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    private void Start()
    {
        //Get interaction components
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        input = new CustomActions();
        AssignInputs();
    }
    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            agent.destination = hit.point;
            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
    void Update()
    {
        FaceTarget();
        SetAnimations();
        if(Input.GetKeyDown(KeyCode.B))
        {
            UI.ToggleInventoryPanel();
        }

        //Runs the function that handles all the interaction
        Interact();
    }

    public void Interact()
    {
        //Tool interaction
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Interact
            Debug.Log("tes");
            playerInteraction.Interact();
        }

        //Item Interaction
        if (Input.GetButtonDown("Fire2"))
        {
            playerInteraction.ItemInteract();
        }

        //Item Keep
        if (Input.GetButtonDown("Fire3"))
        {
            playerInteraction.ItemKeep();
        }
    }
    void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }
    void SetAnimations()
    {
        if (agent.velocity == Vector3.zero)
        {
            animator.Play(IDLE);
        }
        else
        {
            animator.Play(WALK);
        }
    }
}