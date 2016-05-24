using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_validator
{
    enum Marker : int
    {
        // 0xFF00 - 0xFFD7
        STARTOFIMAGE = 0xFFD8,
        ENDOFIMAGE = 0xFFD9,
        STARTOFSTREAM = 0xFFDA,
        // 0xFFDB - 0xFFDF
        APP0 = 0xFFE0,
        APP1 = 0xFFE1,
        APP2 = 0xFFE2,
        APP3 = 0xFFE3,
        APP4 = 0xFFE4,
        APP5 = 0xFFE5,
        APP6 = 0xFFE6,
        APP7 = 0xFFE7,
        APP8 = 0xFFE8,
        APP9 = 0xFFE9,
        APP10 = 0xFFEA,
        APP11 = 0xFFEB,
        APP12 = 0xFFEC,
        APP13 = 0xFFED,
        APP14 = 0xFFEE,
        APP15 = 0xFFEF
        // 0xFFF0 - 0xFFFF
    };
}
