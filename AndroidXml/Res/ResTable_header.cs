// Copyright (c) 2012 Markus Jarderot
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System;

namespace AndroidXml.Res
{
#if !NETSTANDARD1_5
    [Serializable]
#endif
    public class ResTable_header
    {
        public ResChunk_header Header { get; set; }
        public uint PackageCount { get; set; }
    }
}