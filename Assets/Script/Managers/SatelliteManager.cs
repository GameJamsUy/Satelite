using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SatelliteManager
{

    private List<Satellite> satellites;
    private int length;

    public SatelliteManager()
    {
        satellites = new List<Satellite>();
        length = 0;
    }

    public int GetLength()
    {
        return this.length;
    }

    public List<Satellite> GetSatellites()
    {
        return this.satellites;
    }

    public void Add(Satellite satellite)
    {
        if (this.satellites.Contains(satellite))
        {
            throw new ArgumentException("The satellite is already contained in the list.");
        }
        this.satellites.Add(satellite);
        this.length = this.length + 1;
    }

    private void RemoveAt(int index)
    {
        if (index < 0 || index >= length)
        {
            throw new ArgumentException("Invalid index: " + index);
        }
        this.satellites.RemoveAt(index);
        this.length = this.length - 1;
    }

    private void Remove(Satellite satellite)
    {
        if (!this.satellites.Contains(satellite))
        {
            throw new ArgumentException("Satellite not found");
        }
        this.satellites.Remove(satellite);
    }

    public void Destroy()
    {
        foreach (Satellite currSatellite in this.satellites)
        {
            if (currSatellite != null)
            {
                //currSatellite.Destroy();
            }
        }
        this.satellites.Clear();
        this.satellites = new List<Satellite>();
        this.length = 0;
    }
}