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


        if (isTalking && tryToTalk && !!_currentDialogTarget)
        {
            DialogElapsed = Time.time - DialogRefTime;
            if (DialogElapsed >= DialogWaitTime)
            {
                // TALK
                Talk();
                DialogRefTime = Time.time;
            }
            return;
        }



        if (tryToTalk && !isTalking && !_currentDialogTarget && (GOInTalkingRange.Count > 0))
        {
            InitDialog( GOInTalkingRange[0] );
            return;
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

        TalkativeController talkativeController = go.GetComponent<TalkativeController>();
        if (!talkativeController)
            return;

        bool isInRange = false;
        foreach (GameObject g in GOInTalkingRange)
            isInRange = (g.Equals(go));
        if (!isInRange)
            GOInTalkingRange.Add(go);
    }

    int InitDialog(GameObject colliderGO)
    {
        Debug.Log(".... Talking --- INIT ");

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
