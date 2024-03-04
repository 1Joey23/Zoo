using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilites;

namespace CagedItems
{
    public interface ICageable
    {
        double DisplaySize { get; }
        string ResourceKey { get; }
        int XPosition { get; }
        int YPosition { get; }
        HorizontalDirection XDirection { get; }
        VerticalDirection YDirection { get; }
    }
}
