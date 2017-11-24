using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public GameObject slope;
    private bool isInAir;


    void Start()
    {
        if (!slope)
        {
            Debug.Log("Aright");
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        float xForce = Input.GetAxis("Horizontal") * Time.deltaTime * 100;
        float zForce = Input.GetAxis("Vertical") * Time.deltaTime * 1000;
        if (isInAir == false)
        {

            if (xForce != 0)
            {
                gameObject.transform.Rotate(new Vector3(slope.transform.rotation.x, gameObject.transform.rotation.y + xForce, slope.transform.rotation.z));
            }
            if (zForce != 0)
            {
                gameObject.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * zForce, ForceMode.Force);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 15, 0), ForceMode.Impulse);
            }

        } else
        {
            xForce *= .1f;
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
                gameObject.transform.Rotate(new Vector3(gameObject.transform.rotation.x + 2, 0, 0));
            } else if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.Rotate(new Vector3(gameObject.transform.rotation.x - 2, 0, 0));
            } 
        }

    }

   

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            gameObject.transform.rotation = slope.transform.rotation;
            isInAir = false;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isInAir = true;
        }
    }

}
