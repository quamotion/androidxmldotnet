using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidXml.Res
{
    [Serializable]
    public class ResTable_map_entry : ResTable_entry
    {
        public ResTable_ref Parent { get; set; }
        public uint Count { get; set; }
    }
}
