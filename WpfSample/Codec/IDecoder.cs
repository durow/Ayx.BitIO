﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.Codec
{
    public interface IDecoder
    {
        string Decode(byte[] data);
    }
}
