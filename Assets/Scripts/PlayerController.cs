using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;        
    public float groundDist = 0.3f; 
    public LayerMask terrainLayer;  
    
    public Rigidbody rb;
    public SpriteRenderer sr;
    [SerializeField] private Animator anim;
    
    
    private Vector3 moveInput;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (rb == null) Debug.LogError("Rigidbody component is missing on the Player!");
        if (sr == null) Debug.LogError("SpriteRenderer component is missing on the Player!");
    }

    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveInput = new Vector3(x, 0, y);

        
        if (x < 0)
        {
            sr.flipX = true;
        }
        else if (x > 0)
        {
            sr.flipX = false;
        }

       
        if (anim != null)
        {
            bool isMoving = Mathf.Abs(x) > 0.01f || Mathf.Abs(y) > 0.01f;
            anim.SetBool("isRunning", isMoving);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {
      
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1; 
        
        float currentYVelocity = rb.linearVelocity.y;
        
        bool isGrounded = Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer);

        if (isGrounded)
        {

            Vector3 movePos = transform.position; 
            movePos.y = hit.point.y + groundDist;
            transform.position = movePos;

            rb.linearVelocity = new Vector3(moveInput.x * speed, 0, moveInput.z * speed);
        }
        else
        {

            rb.linearVelocity = new Vector3(moveInput.x * speed, currentYVelocity, moveInput.z * speed);
        }
    }
}