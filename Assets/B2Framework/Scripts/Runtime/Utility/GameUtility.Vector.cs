using UnityEngine;

namespace B2Framework
{
    public static partial class GameUtility
    {
        public static partial class Vector
        {
            public static bool IsNearEqual(Vector3 start, Vector3 end)
            {
                var dis = (end - start).sqrMagnitude;
                Debug.Log(dis);
                return !(dis > GameConst.PRECISION);
                // return start == end;
            }
        }
    }
}