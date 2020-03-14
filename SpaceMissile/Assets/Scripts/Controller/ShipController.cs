using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : SpaceController
{
    Rigidbody body;

    public bool controlEnable;
    public bool mouseVisibility;

    public float speed = 0;
    public float acceleration = 0;
    public float rollFactor = 0;
    public float yawFactor = 0;
    public float pitchFactor = 0;

    public float currSpeed = 0;
    public float maxForwardSpeed = 0;
    public float maxBackwardSpeed = 0;

    public float mouseSensitivity = 0.1f;
    
    float horizontalAxis;
    float verticalAxis;
    float rollAxis;

    public Vector3 controlVector = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        this.body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        if (!this.mouseVisibility)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (this.controlEnable)
        {
            spaceship[0].GetComponent<PlayerShipView>().controlShipExhaustLow();
            //set movement axis
            this.horizontalAxis = Input.GetAxis("Mouse X");
            this.verticalAxis = Input.GetAxis("Mouse Y");
            this.rollAxis = Input.GetAxis("Horizontal");

            //Constraints
            this.horizontalAxis = Mathf.Clamp(this.horizontalAxis, -this.yawFactor, this.yawFactor);
            this.verticalAxis = Mathf.Clamp(this.verticalAxis, -this.pitchFactor, this.pitchFactor);

            this.controlVector.x -= this.verticalAxis * this.mouseSensitivity;
            this.controlVector.y += this.horizontalAxis * this.mouseSensitivity;
            //this.controlVector.z = -this.rollAxis * this.rollFactor;
            if (Input.GetKey("a"))
            {
                this.controlVector.z -= this.rollAxis * this.rollFactor;
            }
            else if (Input.GetKey("d"))
            {
                this.controlVector.z -= this.rollAxis * this.rollFactor;
            }           

            this.body.angularVelocity = transform.TransformDirection(this.controlVector);

            if (Input.GetKey("w"))
            {
                this.currSpeed += this.acceleration * Time.deltaTime;
                this.currSpeed = Mathf.Clamp(this.currSpeed, this.maxBackwardSpeed, this.maxForwardSpeed);
                
                spaceship[0].GetComponent<PlayerShipView>().controlShipExhaustForward();
            }
            else if (Input.GetKey("s"))
            {
                if (this.currSpeed > 0.5f || this.currSpeed < 0)
                {
                    this.currSpeed -= this.acceleration * Time.deltaTime;
                }
                else
                {
                    this.currSpeed = 0;
                }
                this.currSpeed = Mathf.Clamp(this.currSpeed, this.maxBackwardSpeed, this.maxForwardSpeed);
            }                       

            this.body.velocity = transform.TransformDirection(new Vector3(0, 0, this.currSpeed));
        }
    }
}
