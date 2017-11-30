using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject slope;

    private bool isInAir;

    private RaycastHit hit;

    private const float xForceMultiplier = 100;

    private const float zForceMultiplier = 10000;

    private bool isTurning = false;

    void AlignToTerrain()
    {
        if (!isTurning)
        {

       
        Ray ray = new Ray(transform.position, -transform.up);
        Quaternion rot;
        if (Physics.Raycast(ray, out hit))

        {
            
            rot = Quaternion.FromToRotation(transform.up, hit.normal);

            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            
        }
        }
    }

    void FixedUpdate()
    {
        if (gameObject != null)
        {


            float xForce = Input.GetAxis("Horizontal") * Time.deltaTime * xForceMultiplier;
            float zForce = Input.GetAxis("Vertical") * Time.deltaTime * zForceMultiplier;



            Debug.Log("Aright");
            if (isInAir == false)
            {
                    AlignToTerrain();
                 if (xForce == 0)
                {
                    isTurning = false;
                } else
                {
                    isTurning = true;
                }
                    gameObject.transform.Rotate(new Vector3(transform.rotation.x, gameObject.transform.rotation.y + xForce, transform.rotation.z));
                    gameObject.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * zForce, ForceMode.Force);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 15, 0), ForceMode.Impulse);
                    xForce *= .01f;
                    zForce *= .01f;
                }

            }
            else
            {

                //Test Code

                if (xForce != 0)
                {
                    // gameObject.transform.Rotate(new Vector3(slope.transform.rotation.x, gameObject.transform.rotation.y + xForce, slope.transform.rotation.z));
                    if (Input.GetKey(KeyCode.D))
                    {

                        gameObject.GetComponent<Rigidbody>().AddTorque((transform.up * xForce), ForceMode.VelocityChange);
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * xForce, ForceMode.VelocityChange);
                    }

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
