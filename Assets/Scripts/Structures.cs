using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;


public enum StructureChoice {
    Dolmen,
    Gateway,
    Guillotine,
    Statue,
    Temple2,
    Torii,
    StructureChoiceSize
}

public enum StructureType
{
    Easy,
    Medium,
    Advanced
}



public class Structure
{
    public float originAngle;
    public (int, int)[] points;
    public List<int> availablePoints;
    public Vector2 size = Vector2.zero;
    public StructureType type;




    public bool IsStructFull()
    {
        return availablePoints.Count < 1;
    }
    public (int, int) GetRandomAvailablePoint()
    {
        if (IsStructFull())
            throw new Exception("No available point found !");
        int pointIndex = availablePoints[Random.Range(0, availablePoints.Count)];
        availablePoints.Remove(pointIndex);
        return points[pointIndex];
    }

    public void ReleasePoint(Vector3 goal)
    {
        for (int i=0; i<points.Length; i++)
        {
            var p = points[i];
            if (p.Item1 == goal.x && p.Item2 == goal.y)
                availablePoints.Add(i);
        }
    }

    private void GetSize()
    {
        foreach (var p in points)
        {
            if (p.Item1 > size.x)
                size.x = p.Item1;
            if (p.Item2 > size.y)
                size.y = p.Item2;
        }
    }

    public static Structure GetDolmen()
    {
        Structure dolmen = new Structure();
        dolmen.points = new[] {(4,0), (6,0), (3,1), (4,1), (5,1), (6,1), (7,1)};
        dolmen.availablePoints = new List<int>();
        for (int i = 0; i < 7; i++)
            dolmen.availablePoints.Add(i);
        dolmen.GetSize();
        dolmen.type = StructureType.Easy;
        return dolmen;
    }


    public static Structure GetGateway()
    {
        Structure structure = new Structure();
        structure.points = new[]
        {
            (1,0), (2,0), (3,0), (4,0), (5,0), (6,0), (7,0), (9,0),
            (2,1), (3,1), (4,1), (5,1), (6,1), (7,1),
            (3,2), (6,2),
            (3,3), (6,3),
            (3,4), (4,4), (6,4),
            (3,5), (4,5), (5,5), (6,5),
            (3,6), (4,6), (5,6), (6,6)
        };
        structure.availablePoints = new List<int>();
        for (int i = 0; i < structure.points.Length; i++)
            structure.availablePoints.Add(i);
        structure.GetSize();
        structure.type = StructureType.Advanced;
        return structure;
    }

    public static Structure GetGuillotine()
    {
        Structure guillotine = new Structure();
        guillotine.points = new[]
        {
            (1,0), (2,0), (3,0), (4,0), (5,0), (6,0), (7,0), (9,0),
            (2,1), (3,1), (4,1), (5,1), (6,1), (7,1),
            (3,2), (6,2),
            (3,3), (6,3),
            (3,4), (4,4), (6,4),
            (3,5), (4,5), (5,5), (6,5),
            (3,6), (4,6), (5,6), (6,6)
        };
        guillotine.availablePoints = new List<int>();
        for (int i = 0; i < guillotine.points.Length; i++)
            guillotine.availablePoints.Add(i);
        guillotine.GetSize();
        guillotine.type = StructureType.Advanced;
        return guillotine;
    }
}
