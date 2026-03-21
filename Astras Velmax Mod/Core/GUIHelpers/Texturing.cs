using UnityEngine;

namespace Astras_Velmax_Mod.Core.GUIHelpers;

    public static class Texturing
    {
        public static Texture2D MakeTextures(int HH, int WW, Color Col)
        {
            Texture2D MAKE = new Texture2D(HH, WW);
            MAKE.SetPixel(0, 0, Col);
            MAKE.Apply();
            return MAKE;
        }
    }

