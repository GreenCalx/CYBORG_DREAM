using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogController : MonoBehaviour
{

    private bool tryToTalk = false;
    private bool isTalking = false;
    private Transform talkTransform;
    private TalkativeController _currentDialogTarget;
    private List<GameObject> GOInTalkingRange;

    public float DialogWaitTime = 1.0f ;
    private float DialogRefTime = 0.0f;
    private float DialogElapsed = 0.0f;


    // Use this for initialization
    void Start()
    {
        talkTransform = GetComponent<Transform>();
        GOInTalkingRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
            
        tryToTalk = Input.GetButton("Talk");

        if ( isTalking && tryToTalk && !!_currentDialogTarget )
        {
            DialogElapsed = DialogRefTime - Time.time;
            if ( DialogElapsed >= DialogWaitTime )
            {
                // TALK
                Talk();
                DialogRefTime = Time.time;
            }
                
        }
            
    }

    void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if ( !!go )
        {
            bool isInRange = false;
            foreach (GameObject g in GOInTalkingRange)
                isInRange = (g.Equals(go));
            if (isInRange)
                GOInTalkingRange.Remove(go);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        bool isInRange = false;
        foreach (GameObject g in GOInTalkingRange)
            isInRange = (g.Equals(go));
        if (!isInRange)
            GOInTalkingRange.Add(go);
        
        // Poll for talk
        if (tryToTalk)
            InitDialog(other);
    }

    int InitDialog(Collider other)
    {
        Debug.Log(".... Talking --- INIT ");

        GameObject colliderGO = other.gameObject;
        if (!colliderGO)
            return -1;
        Debug.Log("Talking to : " + colliderGO.name);
        TalkativeController talkativeNPC = colliderGO.GetComponent<TalkativeController>();
        if (!!talkativeNPC)
        {
            talkativeNPC.ping();
            isTalking = !talkativeNPC.isDialogOver();
            if (!isTalking)
                _currentDialogTarget = null;
            else
                _currentDialogTarget = talkativeNPC;
            DialogRefTime = Time.time;
        }

        return 0;
    }

    int Talk()
    {
        Debug.Log(".... Talking");

        if (!!_currentDialogTarget)
        {
            _currentDialogTarget.ping();
            isTalking = !_currentDialogTarget.isDialogOver();
            if (!isTalking)
                _currentDialogTarget = null;

        }

        return 0;
    }
}
