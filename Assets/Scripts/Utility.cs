using UnityEngine;

    public static class Utility
    {
        public static Vector3 MouseWorldPos(float z = 0)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(position.x, position.y, z);
        }
    }
