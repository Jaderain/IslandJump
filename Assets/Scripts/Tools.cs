using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Tools {

    public static GameController gc { get; private set; }

    static Tools()
    {
        // get game controller
        GameObject gameCtonrollerObject = GameObject.FindWithTag("GameController");
        if (gameCtonrollerObject != null)
        {
            gc = gameCtonrollerObject.GetComponent<GameController>();
        }

        if (gc == null)
        {
            Debug.LogError("gameController is missing!");
        }
    }

    public static int randomNumberExcept(int n, int[] excludes)
    {
        var exclude = new HashSet<int>();
        foreach (var item in excludes)
        {
            exclude.Add(item);
        }

        List<int> range = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (!exclude.Contains(i)) range.Add(i);
        }

        var rand = new System.Random();
        int index = rand.Next(0, n - exclude.Count);
        return range.ElementAt(index);
    }
}
