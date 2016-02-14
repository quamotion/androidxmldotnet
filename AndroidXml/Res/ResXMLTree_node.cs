// Copyright (c) 2012 Markus Jarderot
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System;

namespace AndroidXml.Res
{
    [Serializable]
    public class ResXMLTree_node
    {
        public ResChunk_header Header { get; set; }
        public uint LineNumber { get; set; }
        public ResStringPool_ref Comment { get; set; }
    }
}