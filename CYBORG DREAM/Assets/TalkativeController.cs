using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GRAPH;
using Assets.GUI;

public class TalkativeController : MonoBehaviour
{

    bool cycleAnimationUp = false;
    private bool dialogIsOver = true;

    public Window _GUIDialogWindow;
    public DialogWindow _dialogGO;
    GameObject mainDialog;

    DialogGraph _dialogGraph;
    private string _message;
    private int _currentDialogNodeID;

    public bool _inDialog;


    public bool isDialogOver()
    {
        return dialogIsOver;
    }

    // Use this for initialization
    void Start()
    {

        _message = " Hello World!";

        //Graph g = new Graph(Application.dataPath +"/"+"Dialogs/test_dialog.xml");
        _dialogGraph = new DialogGraph(
            Application.dataPath + "/" + "Dialogs/test_dialog.xml",
            Application.dataPath + "/" + "Dialogs/test_dialog_content.xml"
            );

        if (!!_GUIDialogWindow)
            _GUIDialogWindow._show = true;

        mainDialog = GameObject.FindGameObjectWithTag("mainDialog");
        if (!!mainDialog)
        {
            _dialogGO = mainDialog.GetComponent<DialogWindow>();
            mainDialog.SetActive(false);
            _inDialog = false;
        }

        // Init dialog to 0 so the first nextmessage reads the right ID.
        _currentDialogNodeID = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (!!mainDialog)
            mainDialog.SetActive(_inDialog);

        if (!!_dialogGO)
            _dialogGO._text.text = _message;
    }



    public void getNextMessage()
    {
        // TODO : make the dialog branching here.
        _currentDialogNodeID++;
        DialogNode n = _dialogGraph._graphNodes[_currentDialogNodeID];
        if (n != null)
        {
            if (n._succesorsID.Count == 0)
                _message = "";
            else
                _message = _dialogGraph._graphNodes[_currentDialogNodeID].message;
        }

    }

    public void ping()
    {
        if (_inDialog)
        {
            _message = "";
            getNextMessage();
            dialogIsOver = _message.Equals("");
        }

        if (!!dialogIsOver)
        {
            dialogIsOver = false;
            _inDialog = !_inDialog;
            _currentDialogNodeID = 0;
            getNextMessage();
        }

    }
}
