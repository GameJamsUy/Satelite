using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LeverManager
{

    private List<Lever> levers;
    private int length;

    public LeverManager()
    {
        levers = new List<Lever>();
        length = 0;
    }

    public int GetLength()
    {
        return this.length;
    }

    public List<Lever> GetLevers()
    {
        return this.levers;
    }

    public void Add(Lever lever)
    {
        if (this.levers.Contains(lever))
        {
            throw new ArgumentException("The lever is already contained in the list.");
        }
        this.levers.Add(lever);
        this.length = this.length + 1;
    }

    private void RemoveAt(int index)
    {
        if (index < 0 || index >= length)
        {
            throw new ArgumentException("Invalid index: " + index);
        }
        this.levers.RemoveAt(index);
        this.length = this.length - 1;
    }

    private void Remove(Lever lever)
    {
        if (!this.levers.Contains(lever))
        {
            throw new ArgumentException("Lever not found");
        }
        this.levers.Remove(lever);
    }

    public void Destroy()
    {
        foreach (Lever currLever in this.levers)
        {
            if (currLever != null)
            {
                //currLever.Destroy();
            }
        }
        this.levers.Clear();
        this.levers = new List<Lever>();
        this.length = 0;
    }
}