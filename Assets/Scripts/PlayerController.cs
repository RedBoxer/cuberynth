using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        float thirdInput = Input.GetAxis("Fire1");

        //Vector3 currGravity = this.transform.up * -1;

        this.transform.Rotate(this.transform.right, horizontalInput);
        this.transform.Rotate(this.transform.up, verticalInput);
        this.transform.Rotate(this.transform.forward, thirdInput);

    }
       
}
