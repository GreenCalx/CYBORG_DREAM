using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    static float X_COEF = 20.0f;
    static float Y_COEF = -20.0f;
    static float X_COEF_MOUSE = 100.0f;
    static float Y_COEF_MOUSE = 50.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * X_COEF;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * Y_COEF;

        float mouse_horizontal = Input.GetAxis("RStick X") * Time.deltaTime * X_COEF_MOUSE;
        float mouse_vertical = Input.GetAxis("RStick Y") * Time.deltaTime * Y_COEF_MOUSE;
        mouse_vertical = 0; // TODO : FREE THE Y AXIS FOR FREEDOM

        transform.Rotate(mouse_vertical, 0, mouse_horizontal);
        transform.Translate(horizontal, vertical, 0);

	}

    void OnCollisionEnter(Collision collision)
    {

    }
}
