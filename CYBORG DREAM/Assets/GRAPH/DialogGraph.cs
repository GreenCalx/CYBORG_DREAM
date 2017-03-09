using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace Assets.GRAPH
{
    class DialogGraph : Graph<DialogNode>
    {
        protected string _XMLDialogFileName ;

        public DialogGraph(string xmlNodeFileName) : base(xmlNodeFileName)
        {
        }

        public DialogGraph(string xmlNodeFileName, string xmlDialogFileName) : base(xmlNodeFileName)
        {
            _XMLDialogFileName = xmlDialogFileName;
            int RC = 0;

            RC = ConvertGraphNodes();

            if (_XMLDialogFileName != null)
                RC = BuildDialogGraphFromXML();
        }

        public int ConvertGraphNodes()
        {
            return 0;
        }


        public int BuildDialogGraphFromXML()
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument xDoc = new XmlDocument();

            xDoc.Load(_XMLDialogFileName);
            int id = 0;
            foreach (XmlNode node_l1 in xDoc.DocumentElement.ChildNodes)
            {
                if (node_l1.Name == "node")
                {
                    foreach (XmlNode node_l2 in node_l1)
                    {
                        if ( node_l2.Name == "id")
                        {
                            try
                            {
                                id = int.Parse(node_l2.InnerText);
                            }
                            catch (Exception e)
                            {
                                return -1;
                            }
                        }
                        if (node_l2.Name == "message")
                        {
                            _graphNodes[id-1].message = node_l2.InnerText;

                        }
                    }
                }
            }
            return 1;
        }
    }
}
