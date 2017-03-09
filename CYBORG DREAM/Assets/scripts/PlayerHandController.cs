using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHandController : MonoBehaviour {

    private bool tryToGrab = false;

    private bool isGrabbingObject = false;
    private Rigidbody grabbedRB;
    private GameObject grabbedGO;
    private Transform handTransform;

	// Use this for initialization
	void Start () {
        handTransform = GetComponent<Transform>();
        // TODO : Insert failure check
	}
	
	// Update is called once per frame
	void Update () {

        tryToGrab = Input.GetButton("Action1");
        //tryToTalk = Input.GetButton("Talk");

        if (isGrabbingObject)
        {
            Transform grabbedGOTransform = grabbedGO.GetComponent<Transform>();
            if ( !!grabbedGOTransform )
            {
                grabbedGOTransform.position = handTransform.position;
            }
        }
		
	}

    void OnTriggerEnter( Collider other )
    {
        // Poll user action
        if (tryToGrab && !isGrabbingObject)
            Grab(other);
        else if (isGrabbingObject)
            ReleaseGrabbedObject();

        // Poll for talk
        //if (tryToTalk)
        //    Talk(other);

    }

    //int Talk(Collider other)
    //{
    //    Debug.Log(".... Talking");

    //    GameObject colliderGO = other.gameObject;
    //    if (!colliderGO)
    //        return -1;
    //    Debug.Log("Talking to : " + colliderGO.name);
    //    TalkativeController talkativeNPC = colliderGO.GetComponent<TalkativeController>();
    //    if (!!talkativeNPC)
    //    {
    //        talkativeNPC.toggleDialog();
    //    }

    //    return 0;
    //}

    int Grab(Collider other)
    {
        Debug.Log(".... Grabbing");
        // Retrieve the game object
        GameObject colliderGO = other.gameObject;
        if ( !colliderGO )
            return -1;
        // Check if its a grabbable object
        GrabbableObject grabbableObject = colliderGO.GetComponent<GrabbableObject>();
        if (!grabbableObject)
            return 0;

        // Retrieve its Rigidbody and disable gravity on it
        Rigidbody colliderRB = colliderGO.GetComponent<Rigidbody>();
        if (colliderRB)
            colliderRB.useGravity = false;

        // Now we have a grasp on the object, update the members vars.
        isGrabbingObject = true;
        grabbedGO = colliderGO;
        grabbedRB = colliderRB;

        return 1;
    }

    int ReleaseGrabbedObject()
    {
        grabbedGO = null;
        grabbedRB = null;
        isGrabbingObject = false;
        return 1;
    }
}
