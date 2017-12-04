using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject slope;

    private bool isInAir;

    private RaycastHit hit;

    private bool isTurning = false;

    private Vector3 turnAmt;

    GameObject rightTip;

    GameObject rightTail;

    GameObject leftTip;

    GameObject leftTail;

    void Start()
    {
        rightTip = transform.GetChild(1).GetChild(0).gameObject;
        leftTip = transform.GetChild(0).GetChild(0).gameObject;
        rightTail = transform.GetChild(1).GetChild(1).gameObject;
        leftTail = transform.GetChild(0).GetChild(1).gameObject;
    }

    void AlignToTerrain()
    {
        if (!isTurning)
        {
            Ray rightTipRay = new Ray(transform.position, -transform.up);
            Ray rightTailRay = new Ray(transform.position, -transform.up);
            Ray leftTipRay = new Ray(transform.position, -transform.up);
            Ray leftTailRay = new Ray(transform.position, -transform.up);
            if (!(Physics.Raycast(rightTipRay, out hit, 5f) && Physics.Raycast(leftTipRay, out hit, 5f)))
            {
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, -30, 0), rightTip.transform.position);
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, -30, 0), leftTip.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        if (gameObject != null)
        {

            float xForce = Input.GetAxis("Horizontal") * Time.deltaTime * 100000;
            float zForce = Input.GetAxis("Vertical") * Time.deltaTime * 100000;

            if (!isInAir)
            {
                AlignToTerrain();

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    isTurning = true;
                    Debug.Log("A || D Pressed");
                    gameObject.transform.Rotate(new Vector3(transform.rotation.x, gameObject.transform.rotation.y + xForce, transform.rotation.z));
                    turnAmt = transform.rotation.eulerAngles;
                }
                else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * zForce, ForceMode.Force);

                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1500, 0), ForceMode.Impulse);

                }
                else
                {
                    isTurning = false;
                }

            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * xForce, ForceMode.Impulse);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Debug.Log("A In Air");
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * xForce, ForceMode.Impulse);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * zForce, ForceMode.Impulse);
                }
                else if (Input.GetKey(KeyCode.S))
                { 
                    gameObject.GetComponent<Rigidbody>().AddTorque((transform.right * zForce) * 10000, ForceMode.Force);

                }
            }

        }

    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground") && gameObject != null)
        {

            isInAir = false;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground") && gameObject != null)
        {
            isInAir = true;
        }
    }

}
