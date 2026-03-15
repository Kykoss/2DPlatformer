using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DEMO_PlayerController : MonoBehaviour
{
    [Header("Input")] 
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;

    [Header("PlayerData")] 
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpforce = 5f;
    public Vector2 moveDirection;

    [Header("States")] 
    public bool isMoving;
    public bool isRunning;
    public bool isJumping;
    public bool isGrounded;

    [Header("Components")] 
    public Rigidbody2D rb;


    [Header("Debug")] 
    public bool isDebug = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        // Input actions
        moveAction = InputSystem.actions.FindAction("Move");
        runAction = InputSystem.actions.FindAction("Run");
        jumpAction = InputSystem.actions.FindAction("Jump");
        
        // Components
        rb = GetComponent<Rigidbody2D>();


    }

    private void OnEnable()
    {
        // Input actons
        moveAction.started += OnMoveStarted;
        //moveAction.performed += onMove;
        moveAction.canceled += OnMoveCanceled;
        
        

    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            onMove(moveDirection, moveSpeed);
        }
        
    }
    
    // Mapper
    private void onMove(Vector2 direction, float moveSpeed)
    {
        if (isDebug)
        {
            Debug.Log("OnMove is pressed");
        }
        else
        {
            
            Rigidbody2D.SlideMovement slideMovement = new Rigidbody2D.SlideMovement();
            Rigidbody2D.SlideResults slideResults = new Rigidbody2D.SlideResults();
            

            // velo = dir * speed
            Vector2 velocity = new Vector2((direction.x * moveSpeed),0);

            slideResults = rb.Slide(velocity, Time.deltaTime, slideMovement);
            
        }


    }
    
    private void OnMoveStarted(InputAction.CallbackContext ctx)
    {
        moveDirection = ctx.ReadValue<Vector2>();
        isMoving = true;
        
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveDirection = Vector2.zero;
        isMoving = false;
    }
    

}
