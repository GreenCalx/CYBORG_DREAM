using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;

namespace Assets.GRAPH
{

    class Graph<T> where T : Node , new()
    {
        public List<T> _graphNodes;
        public List<Arc> _graphArcs;
        public string _graphLabel;

        public string _JSONNodeFileName;
        public string _XMLNodeFileName;

        public Graph(string xmlNodeFileName)
        {
            int RC = 0;
            _graphNodes = new List<T>();
            _graphArcs = new List<Arc>();
            _XMLNodeFileName = xmlNodeFileName;

            if (_XMLNodeFileName != null)
                RC = BuildGraphFromXML();
            else if (_JSONNodeFileName != null)
                BuildGraphFromJSON();
            if (RC > 0)
                RC = BuildGraph();

        }

        public int BuildGraph()
        {
            // Build nodes
            foreach( T n in _graphNodes)
            {
                foreach (int id in n._predecessorsID)
                    n.addPredecessor(_graphNodes[id-1]);
                foreach (int id in n._succesorsID)
                    n.addSuccessor(_graphNodes[id-1]);
            }
            return 1;


        }

        public int BuildGraphFromXML()
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument xDoc = new XmlDocument();

            xDoc.Load(_XMLNodeFileName);

            // Main Parsing Loop;
            foreach (XmlNode node_l1 in xDoc.DocumentElement.ChildNodes)
            {
                if (node_l1.Name == "GraphLabel")
                    _graphLabel = node_l1.InnerText;

                if (node_l1.Name == "Node")
                {
                    // Create new elem
                    _graphNodes.Add( new T() );
                    int graphLength = _graphNodes.Count;
                    Node currentNode = _graphNodes[graphLength - 1];
                    currentNode._succesorsID = new List<int>();
                    currentNode._predecessorsID = new List<int>();
                    // Serialize
                    foreach (XmlNode node_l2 in node_l1)
                    {
                        if (node_l2.Name == "ID")
                        {
                            try
                            {
                                int el = int.Parse(node_l2.InnerText);
                                currentNode._ID = el;
                            }
                            catch (Exception e)
                            {
                                return -1;
                            }
                        }


                        if (node_l2.Name == "PREDECESSOR_ID")
                        {
                            foreach (XmlNode node_l3 in node_l2)
                            {
                                try
                                {
                                    int el = int.Parse(node_l3.InnerText);
                                    currentNode._predecessorsID.Add(el);
                                }
                                catch (Exception e)
                                {
                                }
                            }
                        }

                        if (node_l2.Name == "SUCCESSOR_ID")
                        {
                            foreach (XmlNode node_l3 in node_l2)
                            {
                                try
                                {
                                    int el = int.Parse(node_l3.InnerText);
                                    currentNode._succesorsID.Add(el);
                                }
                                catch (Exception e)
                                {
                                }
                            }
                        }

                    }

                }

            }


            return 1;
        }

        public int BuildGraphFromJSON()
        {
            //JSONNodeList nl = JsonUtility.FromJson<JSONNodeList>(_JSONNodeFileName);
            return 0;
        }
    }
}
