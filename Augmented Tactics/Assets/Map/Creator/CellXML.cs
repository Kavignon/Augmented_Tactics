﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace Assets.Map.Creator
{
    public class CellXML
    {
        /* [XmlAttribute("id")]
        public int id;*/

        [XmlAttribute("locX")]
        public int locX;

        [XmlAttribute("locY")]
        public int locY;
    }
}
