﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace AssemblyCSharp
{
	public class POINT
	{
		public int x;
		public int y;

		public int distance(POINT P)
		{
			return Convert.ToInt32(System.Math.Sqrt(System.Math.Pow((this.x - P.x), 2) + System.Math.Pow((this.y - P.y), 2)));
		}

		public int AdjacentTile(POINT P)
		{
			int sum1 = this.x + this.y;
			int sum2 = P.x + P.y;
			return Math.Abs (sum1 - sum2);
		}

		public void pos(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}

