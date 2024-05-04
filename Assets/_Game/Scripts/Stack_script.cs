using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack_script : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dashpickup")
        {
            other.gameObject.tag = "normal";
            Movement.instance.PickDash(other.gameObject);
            other.gameObject.AddComponent<Rigidbody>();
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<Stack_script>();
            Destroy(this);
        }
    }

}
