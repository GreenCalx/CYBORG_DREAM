using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

    private List<Node> _successors;
    private List<Node> _predecessors;

    public List<int> _succesorsID;
    public List<int> _predecessorsID;

    public  int _ID;

    public Node()
    {
        _successors = new List<Node>();
        _predecessors = new List<Node>();
    }

    public Node(ref List<Node> predecessors, ref List<Node> successors)
    {
        setPredecessors(ref predecessors);
        setSuccessors(ref successors);
    }

    public int addSuccessor(Node successor)
    {
        if ( _successors != null )
        {
            _successors.Add(successor);
            return 1;
        }
        return -1 ;
    }

    public int addPredecessor(Node predecessor)
    {
        if (_predecessors != null)
        {
            _predecessors.Add(predecessor);
            return 1;
        }
        return -1;
    }


    public int getSuccessors(ref List<Node> successors)
    {
        if (_successors != null)
        {
            successors = _successors;
            return 1;
        }
        return -1;
    }

    public int getPredecessors(ref List<Node> predecessors)
    {
        if (_predecessors != null)
        {
            predecessors = _predecessors;
            return 1;
        }
        return -1;
    }

    public int setSuccessors(ref List<Node> successor)
    {
        if (_successors != null)
        {
            _successors = successor;
            return 1;
        }
        return -1;
    }

    public int setPredecessors(ref List<Node> predecessor)
    {
        if (_successors != null)
        {
            _predecessors = predecessor;
            return 1;
        }
        return -1;
    }

}
