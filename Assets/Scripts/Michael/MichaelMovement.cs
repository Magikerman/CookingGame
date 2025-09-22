using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MichaelMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    [SerializeField] private LayerMask rayIgnore;
    [SerializeField] private float speed;

    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;

    private Vector3 moveDirection;

    
    [SerializeField] private bool isGrounded;
    [Header("Ground Stuff")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float checkToGroundDistance;
    [SerializeField] private float playerHeight;
    [SerializeField] private float groundDrag;

    [Header("Grab Stuff")]
    [SerializeField] private GameObject grabPos;
    [SerializeField] private float grabRange;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask grabLayer;
    [SerializeField] private GameObject thingGrabbed;
    public GameObject ThingGrabbed => thingGrabbed;
    private bool dontGrab;
    

    [Header("Throw Stuff")]
    private float forceThrow;
    [SerializeField] private float throwMult;
    [SerializeField] private float maxThrowForce;
    [SerializeField] private float timerToThrow;
    private float timer;
    private Collider grabbedObjectCollider;
    [SerializeField] private Slider throwSlider;
    private bool buttonPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        throwSlider.gameObject.SetActive(false);
        timer = timerToThrow;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + checkToGroundDistance, whatIsGround);

        MyInput();

        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }   
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        vertical = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;

        RaycastHit hit = default;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        Physics.Raycast(ray, out hit, grabRange, ~rayIgnore);

        if (Input.GetAxisRaw("Fire1") == 1)
        {
            if (thingGrabbed != null)
            {
                //throw timer and force
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    throwSlider.gameObject.SetActive(true);
                    forceThrow += throwMult;
                    if (forceThrow >= maxThrowForce)
                    {
                        forceThrow = maxThrowForce;
                    }
                    throwSlider.value = forceThrow / maxThrowForce;
                }
            }
            if (!buttonPressed)
            {
                //Stack food
                if (hit.collider != null && thingGrabbed != null && hit.collider.gameObject != thingGrabbed)
                {
                    var hitFoodStack = hit.collider.gameObject.GetComponent<FoodStack>();
                    var grabbedFoodStack = thingGrabbed.gameObject.GetComponent<FoodStack>();

                    if (hitFoodStack != null && grabbedFoodStack != null)
                    {
                        hitFoodStack.AddToStack(grabbedFoodStack);
                        thingGrabbed = null;
                        grabbedObjectCollider.enabled = true;
                        dontGrab = true;
                    }
                }
            }
            buttonPressed = true;
        }

        if (Input.GetAxisRaw("Fire1") == 0 && buttonPressed == true)
        {
            bool throwThing = false;
            timer = timerToThrow;
            if (hit.collider != null)
            {
                if (thingGrabbed == null)
                {
                    //Grab
                    if (grabLayer == (1 << hit.collider.gameObject.layer) && !dontGrab)
                    {
                        SendGrab(hit.collider.gameObject);
                        var foodStack = thingGrabbed.gameObject.GetComponent<FoodStack>();
                        if (foodStack != null && foodStack.Root != foodStack) { foodStack.RemoveFromStack(); }
                    }
                    //Interact
                    else if (hit.collider.gameObject.GetComponent<ITouchable>() != null)
                    {
                        hit.collider.gameObject.GetComponent<ITouchable>().Interact(gameObject);
                    }
                }
                else { throwThing = true; }
            }
            if (thingGrabbed != null && (hit.collider == null || throwThing))
            {
                throwSlider.gameObject.SetActive(false);
                thingGrabbed.transform.parent = null;
                thingGrabbed.AddComponent<Rigidbody>();
                thingGrabbed.GetComponent<Rigidbody>().AddForce(playerCamera.forward * forceThrow, ForceMode.Force);
                forceThrow = 0;
                grabbedObjectCollider.enabled = true;
                thingGrabbed = null;
            }
            buttonPressed = false;
            dontGrab = false;
        }
            
        if (Input.GetKeyDown("space"))
        {
            TextManager.Instance.NextLine();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);
    }

    public void SendGrab(GameObject hit)
    {
        hit.transform.parent = grabPos.transform;
        thingGrabbed = hit;

        if (thingGrabbed != null)
        {
            thingGrabbed.transform.localPosition = new Vector3(0, 0, 0);
            thingGrabbed.transform.localRotation = Quaternion.Euler(0, 0, 0);
            grabbedObjectCollider = thingGrabbed.GetComponent<Collider>();
            grabbedObjectCollider.enabled = false;
            Destroy(thingGrabbed.GetComponent<Rigidbody>());
        }
    }
}
