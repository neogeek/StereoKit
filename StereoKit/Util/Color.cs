﻿using System.Runtime.InteropServices;

namespace StereoKit
{
    /// <summary>A 32 bit color struct! This is often directly used by StereoKit data
    /// structures, and so is often necessary for setting texture data, or mesh data.
    /// Note that the Color type implicitly converts to Color32, so you can use the 
    /// static methods there to create Color32 values!</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Color32
    {
        public byte r, g, b, a;
        public Color32(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>Pure opaque white! Same as (255,255,255,255).</summary>
        public static readonly Color32 White = new Color32(255, 255, 255, 255);
    }

    /// <summary>A color value stored as 4 floats with values that are generally between
    /// 0 and 1! Note that there's also a Color32 structure, and that 4 floats is generally
    /// a lot more than you need. So, use this for calculating individual colors at quality,
    /// but maybe store them en-masse with Color32!
    /// 
    /// Also note that RGB is often a terrible color format for picking colors, but it's how
    /// our displays work and we're stuck with it. If you want to create a color via code, 
    /// try out the static Color.HSV method instead!</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        /// <summary>Pure opaque white! Same as (1,1,1,1).</summary>
        public static readonly Color White = new Color(1,1,1,1);
        /// <summary>Pure opaque black! Same as (0,0,0,1).</summary>
        public static readonly Color Black = new Color(0,0,0,1);
        /// <summary>Pure transparent black! Same as (0,0,0,0).</summary>
        public static readonly Color BlackTransparent = new Color(0,0,0,0);

        /// <summary>Red component, a value that is generally between 0-1</summary>
        public float r;
        /// <summary>Green component, a value that is generally between 0-1</summary>
        public float g;
        /// <summary>Blue component, a value that is generally between 0-1</summary>
        public float b;
        /// <summary>Alpha, or opacity component, a value that is generally between 0-1, where 0 is 
        /// completely transparent, and 1 is completely opaque.</summary>
        public float a;

        /// <summary>Try Color.HSV instead! But if you really need to create a color from RGB
        /// values, I suppose you're in the right place. All parameter values are generally in
        /// the range of 0-1.</summary>
        /// <param name="red">Red component, 0-1.</param>
        /// <param name="green">Green component, 0-1.</param>
        /// <param name="blue">Blue component, 0-1.</param>
        /// <param name="opacity">Opacity, or the alpha component, 0-1 where 0 is completely 
        /// transparent, and 1 is completely opaque.</param>
        public Color(float red, float green, float blue, float opacity=1)
        {
            this.r = red;
            this.g = green;
            this.b = blue;
            this.a = opacity;
        }

        /// <summary>Converts the color to a Hue/Saturation/Value format! Does not consider
        /// transparency when calculating the result.</summary>
        /// <returns>Hue, Saturation, and Value, stored in x, y, and z respectively. All
        /// values are between 0-1.</returns>
        public Vec3 ToHSV()
            => NativeAPI.color_to_hsv(this);

        /// <summary>Converts the RGB color to a CIE LAB color space value! Conversion back and forth
        /// from LAB space could be somewhat lossy.</summary>
        /// <returns>An LAB vector where x=L, y=A, z=B.</returns>
        public Vec3 ToLAB()
            => NativeAPI.color_to_lab(this);

        /// <summary>Creates a Red/Green/Blue color from Hue/Saturation/Value information.</summary>
        /// <param name="hue">Hue most directly relates to the color as we think of it! 0 is red,
        /// 0.1667 is yellow, 0.3333 is green, 0.5 is cyan, 0.6667 is blue, 0.8333 is magenta, and 1
        /// is red again!</param>
        /// <param name="saturation">The vibrancy of the color, where 0 is straight up a shade of gray,
        /// and 1 is 'poke you in the eye colorful'.</param>
        /// <param name="value">The brightness of the color! 0 is always black.</param>
        /// <param name="opacity">Also known as alpha! This is does not affect the rgb components of the
        /// resulting color, it'll just get slotted into the colors opacity value.</param>
        /// <returns></returns>
        public static Color HSV(float hue, float saturation, float value, float opacity = 1)
            => NativeAPI.color_hsv(hue, saturation, value, opacity);

        public static Color HSV(Vec3 hsvColor, float opacity = 1)
            => NativeAPI.color_hsv(hsvColor.x, hsvColor.y, hsvColor.z, opacity);

        /// <summary>Creates an RGB color from a CIE-L*ab color space. CIE-L*ab is a color space that models
        /// human perception, and has significantly more accurate to perception lightness values, so this is 
        /// an excellent color space for color operations that wish to preserve color brightness properly. 
        /// Traditionally, values are L [0,100], a,b [-200,+200] but here we normalize them all to the 0-1
        /// range. If you hate it, let me know why!</summary>
        /// <param name="l">Lightness of the color! Range is 0-1.</param>
        /// <param name="a">'a' is from red to green. Range is 0-1.</param>
        /// <param name="b">'b' is from blue to yellow. Range is 0-1.</param>
        /// <param name="opacity">The opacity copied into the final color!</param>
        /// <returns>An RGBA color constructed from the LAB values.</returns>
        public static Color LAB(float l, float a, float b, float opacity = 1)
            => NativeAPI.color_lab(l, a, b, opacity);

        public static implicit operator Color32(Color c) 
            => new Color32((byte)(c.r*255), (byte)(c.g*255), (byte)(c.b*255), (byte)(c.a*255));
        public static Color operator *(Color a, float b) { return new Color(a.r * b, a.g * b, a.b * b, a.a * b); }
    }
}
