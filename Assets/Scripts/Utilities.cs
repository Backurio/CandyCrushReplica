using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static IEnumerator AnimatePotentialMatches(IEnumerable<GameObject> potentialMatches)
	{
		for (float i = 1.0f; i >= 0.3f; i -= 0.1f)
		{
			foreach (var item in potentialMatches)
			{
				Color c = item.GetComponent<SpriteRenderer>().color;
				c.a = i;
				item.GetComponent<SpriteRenderer>().color = c;
			}
			yield return new WaitForSeconds(Constants.OpacitAnimationFrameDelay);
		}

		for (float i = 0.3f; i <= 1.0f; i += 0.1f)
		{
			foreach (var item in potentialMatches)
			{
				Color c = item.GetComponent<SpriteRenderer>().color;
				c.a = i;
				item.GetComponent<SpriteRenderer>().color = c;
			}
			yield return new WaitForSeconds(Constants.OpacitAnimationFrameDelay);
		}
	}


	public static bool AreVerticalOrHorizontalNeighbors(Shape s1, Shape s2)
	{
		return (((s1.Column == s2.Column) || (s1.Row == s2.Row)) && (Mathf.Abs(s1.Column - s2.Column) <= 1) && (Mathf.Abs(s1.Row - s2.Row) <= 1));
	}

	public static IEnumerable<GameObject> GetPotentialMatches(ShapesArray shapes)
	{
		// todo: continue here
	}
}
