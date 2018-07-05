using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapesArray
{
	private GameObject[,] shapes = new GameObject[Constants.Rows, Constants.Columns];
	private GameObject backupG1;
	private GameObject backupG2;

	public GameObject this[int row, int column]
	{
		get
		{
			// throws an error if index is out of array range
			try
			{
				return shapes[row, column];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		set
		{
			shapes[row, column] = value;
		}
	}

	public void Swap(GameObject g1, GameObject g2)
	{
		// hold a backup in case no match is produced
		backupG1 = g1;
		backupG2 = g2;

		var g1Shape = g1.GetComponent<Shape>();
		var g2Shape = g2.GetComponent<Shape>();

		// get array indexes
		int g1Row = g1Shape.Row;
		int g1Column = g1Shape.Column;
		int g2Row = g2Shape.Row;
		int g2Column = g2Shape.Column;

		// swap them in the array
		var temp = shapes[g1Row, g1Column];
		shapes[g1Row, g1Column] = shapes[g2Row, g2Column];
		shapes[g2Row, g2Column] = temp;

		// swap their respective properties
		Shape.SwapColumnRow(g1Shape, g2Shape);
	}

	public void UndoSwap()
	{
		if ((backupG1 == null) || (backupG2 == null))
		{
			throw new Exception("Backup is null");
		}

		Swap(backupG1, backupG2);
	}

	public IEnumerable<GameObject> GetMatches(IEnumerable<GameObject> gos)
	{
		List<GameObject> matches = new List<GameObject>();
		foreach (var go in gos)
		{
			matches.AddRange(GetMatches(go).MatchedCandy);
		}
		return matches.Distinct();
	}

	public MatchesInfo GetMatches(GameObject go)
	{
		MatchesInfo matchesInfo = new MatchesInfo();

		var horizontalMatches = GetMatchesHorizontally(go);
		// todo: continue here
	}

	private IEnumerable<GameObject> GetMatchesHorizontally(GameObject go)
	{
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var shape = go.GetComponent<Shape>();
		// check left
		if (shape.Column != 0)
		{
			for (int column = shape.Column - 1; column >= 0; column--)
			{
				if ((shapes[shape.Row, column] != null) && (shapes[shape.Row, column].GetComponent<Shape>().IsSameType(shape)))
				{
					matches.Add(shapes[shape.Row, column]);
				}
				else
				{
					break;
				}
			}
		}

		// check right
		if (shape.Column != Constants.Columns - 1)
		{
			for (int column = shape.Column + 1; column <= Constants.Columns - 1; column ++)
			{
				if ((shapes[shape.Row, column] != null) && (shapes[shape.Row, column].GetComponent<Shape>().IsSameType(shape)))
				{
					matches.Add(shapes[shape.Row, column]);
				}
				else
				{
					break;
				}
			}
		}

		// we want more than three matches
		if (matches.Count < Constants.MinimumMatches)
		{
			matches.Clear();
		}

		return matches.Distinct();
	}

	private IEnumerable<GameObject> GetMatchesVertically(GameObject go)

	{
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var shape = go.GetComponent<Shape>();
		// check bottom
		if (shape.Row != 0)
		{
			for (int row = shape.Row - 1; row >= 0; row--)
			{
				if ((shapes[row, shape.Column] != null) && (shapes[row, shape.Column].GetComponent<Shape>().IsSameType(shape)))
				{
					matches.Add(shapes[row, shape.Column]);
				}
				else
				{
					break;
				}
			}
		}
		// check top
		if (shape.Row != Constants.Rows - 1)
		{
			for (int row = shape.Row + 1; row < Constants.Rows; row++)
			{
				if ((shapes[row, shape.Column] != null) && (shapes[row, shape.Column].GetComponent<Shape>().IsSameType(shape)))
				{
					matches.Add(shapes[row, shape.Column]);
				}
				else
				{
					break;
				}
			}
		}

		if (matches.Count < Constants.MinimumMatches)
		{
			matches.Clear();
		}

		return matches.Distinct();
	}
}
