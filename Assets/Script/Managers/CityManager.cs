using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CityManager
{

    private List<City> cities;
    private int length;

    public CityManager()
    {
        cities = new List<City>();
        length = 0;
    }

    public int GetLength()
    {
        return this.length;
    }

    public List<City> GetCities()
    {
        return this.cities;
    }

    public void Add(City city)
    {
        if (this.cities.Contains(city))
        {
            throw new ArgumentException("The city is already contained in the list.");
        }
        this.cities.Add(city);
        this.length = this.length + 1;
    }

    private void RemoveAt(int index)
    {
        if (index < 0 || index >= length)
        {
            throw new ArgumentException("Invalid index: " + index);
        }
        this.cities.RemoveAt(index);
        this.length = this.length - 1;
    }

    private void Remove(City city)
    {
        if (!this.cities.Contains(city))
        {
            throw new ArgumentException("City not found");
        }
        this.cities.Remove(city);
    }

    public void Destroy()
    {
        foreach (City currCity in this.cities)
        {
            if (currCity != null)
            {
                //currCity.Destroy();
            }
        }
        this.cities.Clear();
        this.cities = new List<City>();
        this.length = 0;
    }
}