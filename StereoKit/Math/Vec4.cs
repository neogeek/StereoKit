﻿using System;
using System.Runtime.InteropServices;

namespace StereoKit
{
	/// <summary>A vector with 4 components: x, y, z, and w. Can be useful for things like
	/// shaders, where the registers are aligned to 4 float vectors.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Vec4
	{
		/// <summary>Vector components.</summary>
		public float x, y, z, w;
		
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return x;
					case 1:
						return y;
					case 2:
						return z;
					case 3:
						return w;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public Vec2 XY {get=> new Vec2(x, y); set{ x = value.x; y = value.y; }}
		public Vec2 YZ {get=> new Vec2(y, z); set{ y = value.x; z = value.y; }}
		public Vec2 ZW {get=> new Vec2(z, w); set{ z = value.x; w = value.y; }}
		public Vec2 XZ {get=> new Vec2(x, z); set{ x = value.x; z = value.y; }}
        
		/// <summary>A basic constructor, just copies the values in!</summary>
		/// <param name="x">X component of the vector.</param>
		/// <param name="y">Y component of the vector.</param>
		/// <param name="z">Z component of the vector.</param>
		/// <param name="w">W component of the vector.</param>
		public Vec4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/// <summary>A basic constructor, just copies the values in!</summary>
		/// <param name="xyz">X, Y and Z components of the vector.</param>
		/// <param name="w">W component of the vector.</param>
		public Vec4(Vec3 xyz, float w)
		{
			x = xyz.x;
			y = xyz.y;
			z = xyz.z;
			this.w = w;
		}

		/// <summary>A basic constructor, just copies the values in!</summary>
		/// <param name="xy">X and Y components of the vector.</param>
		/// <param name="zw">Z and W components of the vector.</param>
		public Vec4(Vec2 xy, Vec2 zw)
		{
			x = xy.x;
			y = xy.y;
			z = zw.x;
			w = zw.y;
		}

		public override string ToString()
		{
			return string.Format("[{0:0.00}, {1:0.00}, {2:0.00}, {3:0.00}]", x, y, z, w);
		}
	}
}
