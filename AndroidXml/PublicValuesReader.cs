// Copyright (c) 2015 Quamotion
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AndroidXml
{
    public class PublicValuesReader
    {
        public static PublicValuesReader Instance { get; } = new PublicValuesReader(EmbeddedResources.PublicXml);

        public PublicValuesReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            
            XDocument xdoc = XDocument.Load(stream);
            var publicValues = xdoc.Element("resources").Elements("public");
            values = new Dictionary<uint, string>();
            publicValues.ToList().ForEach(pv => AddValue(values, pv));
            this.Values = values;
        }

        public Dictionary<uint, string> Values
        {
            get;
            private set;
        }

        private void AddValue(Dictionary<uint, string> values, XElement publicValue)
        {
            var id = publicValue.Attribute("id");
            var name = publicValue.Attribute("name");
            if (id != null && name != null)
            {
                var identifier = Convert.ToUInt32(id.Value, 16);
                values.Add(identifier, name.Value);
            }
        }
    }
}
