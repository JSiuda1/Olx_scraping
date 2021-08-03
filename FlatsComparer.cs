using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx_scraping
{
    class FlatsComparer : IEqualityComparer<RoomsInfo>
    {
        public bool Equals(RoomsInfo x, RoomsInfo y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            else if(ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }
            else
            {
                return x.Title == y.Title;
            }
        }

        public int GetHashCode(RoomsInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int IDHashCode = obj.GetHashCode();

            int NameHashCode = obj.Title == null ? 0 : obj.Title.GetHashCode();

            return IDHashCode ^ NameHashCode;
        }
    }
}
