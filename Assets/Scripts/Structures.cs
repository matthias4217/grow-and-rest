using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;


public enum StructureChoice {
    Dolmen,
    LittleTree,
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
        List<(int, int)> selectedPoints = new List<(int, int)>();
        int strata = 0;
        foreach (var indexP in availablePoints)
        {
            var p = points[indexP];
            if (selectedPoints.Count == 0 || p.Item2 == strata)
                selectedPoints.Add(p);
            if (p.Item2 < strata)
            {
                selectedPoints = new List<(int, int)>();
                selectedPoints.Add(p);
                strata = p.Item2;
            }
        }
        int pointIndex = availablePoints[Random.Range(0, selectedPoints.Count)];
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

    public static Structure GetLittleTree()
    {
        Structure structure = new Structure();
        structure.points = new[] {(4,0),
            (2,1),
            (3,1),
            (4,1),
            (5,1),
            (6,1),
            (4,2),
            (3,3),
            (4,3),
            (5,3),
            (4,4)
        };
        structure.availablePoints = new List<int>();
        for (int i = 0; i < 7; i++)
            structure.availablePoints.Add(i);
        structure.GetSize();
        structure.type = StructureType.Easy;
        return structure;
    }


    public static Structure GetGateway()
    {
        Structure structure = new Structure();
        structure.points = new[]
        {
            (3,0), (4,0), (5,0), (6,0),
            (2,1), (3,1), (6,1), (7,1),
            (1,2), (2,2), (7,2), (8,2),
            (1,3), (8,3),
            (1,4), (8,4),
            (1,5), (2,5), (7,5), (8,5),
            (2,6), (3,6), (6,6), (7,6),
            (3,7), (4,7), (5,7), (6,7)
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

    public static Structure GetStatue()
    {
        Structure structure = new Structure();
        structure.points = new[]
        {
            (2,0), (3,0), (4,0), (5,0), (6,0),
            (2,1), (3,1), (4,1), (5,1), (6,1),
            (4,2),
            (3,3), (4,3), (5,3),
            (2,4), (3,4), (4,4), (5,4), (6,4),
            (3,5), (4,5), (5,5),
            (4,6)
        };
        structure.availablePoints = new List<int>();
        for (int i = 0; i < structure.points.Length; i++)
            structure.availablePoints.Add(i);
        structure.GetSize();
        structure.type = StructureType.Advanced;
        return structure;
    }

    public static Structure GetTemple2()
    {
        Structure structure = new Structure();
        structure.points = new[]
        {
            (0,0), (1,0), (2,0), (3,0), (4,0), (5,0), (6,0), (7,0), (8,0), (9,0),
            (1,1), (3,1), (6,1), (8,1),
            (1,2), (3,2), (6,2), (8,2),
            (1,3), (3,3), (6,3), (8,3),
            (1,4), (3,4), (6,4), (8,4),
            (0,5), (1,5), (2,5), (3,5), (4,5), (5,5), (6,5), (7,5), (8,5), (9,5),
            (1,6), (2,6), (3,6), (4,6), (5,6), (6,6), (7,6), (8,6),
            (2,7), (3,7), (4,7), (5,7), (6,7), (7,7),
            (3,8), (4,8), (5,8), (6,8)
        };
        structure.availablePoints = new List<int>();
        for (int i = 0; i < structure.points.Length; i++)
            structure.availablePoints.Add(i);
        structure.GetSize();
        structure.type = StructureType.Advanced;
        return structure;
    }

    public static Structure GetTorii()
    {
        Structure structure = new Structure();
        structure.points = new[]
        {
            (3,0), (7,0),
            (3,1), (7,1),
            (3,2), (7,2),
            (1,3), (2,3), (3,3), (4,3), (5,3), (6,3), (7,3), (8,3), (9,3),
            (2,4), (5,4), (8,4),
            (1,5), (2,5), (3,5), (4,5), (5,5), (6,5), (7,5), (8,5), (9,5),
            (1,6), (9,6),
        };
        structure.availablePoints = new List<int>();
        for (int i = 0; i < structure.points.Length; i++)
            structure.availablePoints.Add(i);
        structure.GetSize();
        structure.type = StructureType.Advanced;
        return structure;
    }
}
