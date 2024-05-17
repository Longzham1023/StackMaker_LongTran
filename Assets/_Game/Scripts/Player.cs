using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Rigidbody rb;

    [SerializeField] Transform player;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private bool hasHitWall = false;
    [SerializeField] private Vector3 direction;
    [SerializeField] private List<GameObject> brickList;
    [SerializeField] private TextMeshProUGUI point;
    private int number;

    public GameObject uiElement;
    public float speed;
    public MobleInput input;
    private bool isMoving = false;
    private bool canMove = true;
    public float range = -2.05f;
    float jumpHigh;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!hasHitWall)
        {
            MoveCharacter();
        }
        else
        {
            if (MoveCharacter())
            {
                hasHitWall = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        point.text = " " + number;

        if (canMove)
        {
            MoveCharacter();
        }

        //bat lazer
        Ray theRay = new Ray(transform.position, direction);
        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            //Bat va cham voi tuong
            if (hit.collider.CompareTag("Wall"))
            {
                hasHitWall = true;
                rb.velocity = Vector3.zero;
                isMoving = true;
            }

            //bat va cham va nhat gach
            if (hit.collider.CompareTag("Dashpickup"))
            {
                isMoving = false;
                jumpHigh += 0.3f;
                player.transform.position = new Vector3(transform.position.x, transform.position.y + jumpHigh,
                    transform.position.z);
                //cho vat pham xuong duoi chan
                GameObject brick = Instantiate(brickPrefab, player.transform);
                brick.transform.position = transform.position;
                hit.collider.gameObject.SetActive(false);
                brickList.Add(brick);
                number++;
            }

            //xay cau
            if (hit.collider.CompareTag("Bridge"))
            {
                if(brickList.Count > 0)
                {
                    jumpHigh -= 0.3f;
                    player.transform.position = new Vector3(transform.position.x, transform.position.y + jumpHigh,
                        transform.position.z);
                    hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Destroy(brickList[brickList.Count - 1].gameObject);
                    brickList.RemoveAt(brickList.Count - 1);
                }
                else
                {
                    hasHitWall = true;
                    rb.velocity = Vector3.zero;
                }
            }

            //ve dich
            if (hit.collider.CompareTag("ChessClose"))
            {
                hasHitWall = true;
                rb.velocity = Vector3.zero;
                //hit.collider.gameObject.SetActive(false);
                while (brickList.Count > 0)
                {
                    jumpHigh -= 0.3f;
                    player.transform.position = new Vector3(transform.position.x, transform.position.y + jumpHigh,
                   transform.position.z);
                    Destroy(brickList[brickList.Count - 1].gameObject);
                    brickList.RemoveAt(brickList.Count - 1);
                }
                uiElement.SetActive(true);
                Time.timeScale = 2;
                
            }
            /*if (hit.collider.CompareTag("ChessOpen"))
            {
                Debug.Log(1);
                hit.collider.gameObject.SetActive(true);
            }*/

        }
    }

    public bool MoveCharacter()
    {
        bool movement = false;

        isMoving = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || MobleInput.Instance.GetSwipeDirection() == MobleInput.Direction.Left)
        {
            rb.velocity = Vector3.left * speed * Time.deltaTime;
            direction = Vector3.left;
            movement = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobleInput.Instance.GetSwipeDirection() == MobleInput.Direction.Right)
        {
            rb.velocity = Vector3.right * speed * Time.deltaTime;
            direction = Vector3.right;
            movement = true;

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || MobleInput.Instance.GetSwipeDirection() == MobleInput.Direction.Up)
        {
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
            direction = Vector3.forward;
            movement = true;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || MobleInput.Instance.GetSwipeDirection() == MobleInput.Direction.Down)
        {
            rb.velocity = -Vector3.forward * speed * Time.deltaTime;
            direction = -Vector3.forward;
            movement = true;

        }
        return movement;
    }
}
