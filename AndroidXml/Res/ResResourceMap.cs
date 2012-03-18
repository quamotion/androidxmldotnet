using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidXml.Res
{
    public class ResResourceMap
    {
        public ResChunk_header Header { get; set; }
        public List<uint> ResouceIds { get; set; }

        public string GetResouceName(uint? resourceId, ResStringPool strings)
        {
            if (resourceId == null) return null;
            uint index = 0;
            foreach (var id in ResouceIds)
            {
                if (id == resourceId)
                {
                    return strings.GetString(index);
                }
                index++;
            }
            return null;
        }
    }
}
