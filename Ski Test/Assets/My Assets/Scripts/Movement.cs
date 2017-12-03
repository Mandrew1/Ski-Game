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

    void AlignToTerrain()
    {
        if (!isTurning)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            if (Physics.Raycast(ray, out hit))

            {
                if (transform.rotation.z != Quaternion.FromToRotation(Vector3.up, hit.normal).z)
                {
                    //gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 0, -20), -transform.up, ForceMode.Force);
                    transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal + turnAmt);
                }
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
                
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    isTurning = true;
                    Debug.Log("A || D Pressed");
                    gameObject.transform.Rotate(new Vector3(transform.rotation.x, gameObject.transform.rotation.y + xForce, transform.rotation.z));
                    turnAmt = transform.rotation.eulerAngles;
                } else if  (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * zForce, ForceMode.Force);

                } else if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1500, 0), ForceMode.Impulse);

                } else
                {
                    isTurning = false;
                }

            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    gameObject.GetComponent<Rigidbody>().AddTorque((transform.up * xForce), ForceMode.VelocityChange);
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * xForce, ForceMode.VelocityChange);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * zForce, ForceMode.VelocityChange);
                }

                else if (Input.GetKey(KeyCode.S))
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * zForce, ForceMode.VelocityChange);

                {
                }
            }



            AlignToTerrain();
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
