using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using AndroidXml.Properties;

namespace AndroidXml
{
    public class PublicValuesReader
    {
        private static Dictionary<uint,string> values;

        public static Dictionary<uint,string> Values
        {
            get
            {
                if(values == null)
                {
                    XDocument xdoc = XDocument.Parse(Resources.publicXML);
                    var publicValues = xdoc.Element("resources").Elements("public");
                    values = new Dictionary<uint,string>();
                    publicValues.ToList().ForEach(pv => AddValue(pv));
                }
                return values;
            }
        }

        private static void AddValue(XElement publicValue)
        {
            var id = publicValue.Attribute("id");
            var name = publicValue.Attribute("name");
            if(id != null && name !=null)
            {
                var identifier = Convert.ToUInt32(id.Value, 16);
                values.Add(identifier, name.Value);
            }
        }
    }
}
