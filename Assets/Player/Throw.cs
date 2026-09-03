using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject player;
    public Transform holdPosition;

    public float throwForce = 500f;
    public float pickUpRange = 5f;
    
    private GameObject heldObj;
    private Rigidbody heldObjRb;

    //private int LayerNumber;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(heldObj == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if(hit.transform.gameObject.tag == "CanInteract")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }

                }

            }
            else
            {
                StopClipping();
                DropObject();
            }
        }
        
        if (heldObj != null)
        {
            MoveObject();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StopClipping();
                ThrowObject();
            }

        }

    }

    void PickUpObject(GameObject pickedUpObj)
    {
        if (pickedUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickedUpObj;
            heldObjRb = pickedUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPosition.transform;

            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null; 
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPosition.transform.position;
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

        if(hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }


}
