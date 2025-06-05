using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCursor
{
    public static class CursorUtilities
    {
        private static readonly Dictionary<CursorType, Texture2D> resources = new Dictionary<CursorType, Texture2D>()
        {
            [CursorType.Move] = Load("Cursors/move"),
            [CursorType.Rotate] = Load("Cursors/rotate"),
            [CursorType.Resize] = Load("Cursors/resize"),
        };

        private static Texture2D Load(string filename)
        {
            var tex = Resources.Load<Texture2D>(filename);
            return tex;
        }

        public static void ChangeCursor(CursorType cursor)
        {
            if(cursor == CursorType.Default)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            }
            else
            {
                var tex = resources[cursor];

                Cursor.SetCursor(tex, new Vector2(0.5f * tex.width, 0.5f * tex.height), CursorMode.Auto);
            }
        }
    }

    public enum CursorType
    {
        Default,
        Move,
        Rotate,
        Resize,
    }
}