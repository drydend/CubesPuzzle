using PauseSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using WallsSystem;


public static class HierarchyCustomSelection
{
    [MenuItem("GameObject/Select All Moveable Walls",false, 1)]
    public static void SelectAllMoveableWall()
    {
        SelectObjectsWithComponent<MoveableWall>();
    }

    [MenuItem("GameObject/Select Pause Triggers", false, 1)]
    public static void SelectAllPauseTrigger()
    {
        SelectObjectsWithComponentOfType(typeof(IPauseTrigger));
    }

    [MenuItem("GameObject/Select Unpause Triggers", false, 1)]
    public static void SelectAllUnpauseTrigger()
    {
        SelectObjectsWithComponentOfType(typeof(IUnpauseTrigger));
    }

    private static List<Transform> GetTransfomsWithComponent<T>(Transform gameOjbect)
    {
        var transforms = new List<Transform>();

        for (int i = 0; i < gameOjbect.childCount; i++)
        {
            if (gameOjbect.GetChild(i).TryGetComponent(out T component))
            {
                transforms.Add(gameOjbect.GetChild(i).transform);
            }

            transforms.Add(GetTransfomsWithComponent<T>(gameOjbect.GetChild(i)));
        }

        return transforms;
    }

    private static void SelectObjectsWithComponent<T>() where T : Component
    {
        var selectedObject = Selection.activeGameObject;

        List<Transform> walls = new List<Transform>();

        for (int i = 0; i < selectedObject.transform.childCount; i++)
        {
            walls.Add(GetTransfomsWithComponent<T>(selectedObject.transform.GetChild(i)));
        }

        Selection.objects = walls.Select(transform => transform.gameObject).ToArray();
    }

    private static void SelectObjectsWithComponentOfType(Type type)
    {
        var selectedObject = Selection.activeGameObject;

        List<Transform> walls = new List<Transform>();

        for (int i = 0; i < selectedObject.transform.childCount; i++)
        {
            walls.Add(GetTransfomsWithComponentOfType(type, selectedObject.transform.GetChild(i)));
        }

        Selection.objects = walls.Select(transform => transform.gameObject).ToArray();
    }

    private static List<Transform> GetTransfomsWithComponentOfType(Type type, Transform gameOjbect)
    {
        var transforms = new List<Transform>();

        for (int i = 0; i < gameOjbect.childCount; i++)
        {
            if (gameOjbect.GetChild(i).TryGetComponent(type , out Component component))
            {
                transforms.Add(gameOjbect.GetChild(i).transform);
            }

            transforms.Add(GetTransfomsWithComponentOfType(type,gameOjbect.GetChild(i)));
        }

        return transforms;
    }
}
