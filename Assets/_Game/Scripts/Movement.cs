using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement instance;
    private Rigidbody rb;

    public GameObject DashParent;
    public GameObject PreDash;

    public float speed;

    private bool isMoving = false;
    private Vector3 lastValidPosition;

  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall");
            this.enabled = false;
            rb.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("FinishLine"))
        {
            this.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }



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
        lastValidPosition = transform.position;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || MobleInput.Instance.swipeLeft && isMoving)
        {
            isMoving = true;
            rb.velocity = Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobleInput.Instance.swipeRight && isMoving)
        {
            isMoving = true;
            rb.velocity = Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || MobleInput.Instance.swipeUp && isMoving)
        {
            isMoving = true;
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || MobleInput.Instance.swipeDown && isMoving)
        {
            isMoving = true;
            rb.velocity = - Vector3.forward * speed * Time.deltaTime;
        }
        if(rb.velocity == Vector3.zero)
        {
            isMoving = true;
        }
    }

    public void PickDash(GameObject dashob)
    {
        dashob.transform.SetParent(DashParent.transform);
        Vector3 pos = PreDash.transform.localPosition;
        pos.y -= 1.04f;
        dashob.transform.localPosition = pos;
        Vector3 Charccterpos = transform.localPosition;
        Charccterpos.y += 1.04f;
        transform.localPosition = Charccterpos;
        PreDash = dashob;
        PreDash.GetComponent<BoxCollider>().isTrigger = false;
    }
}
