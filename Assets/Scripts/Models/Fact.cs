using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fact
{
    public int id;
    public Type type;
    public int number;
    public String description;
    public String date;

    public Fact(int id, Type type, int number, String description, String date)
    {
        this.id = id;
        this.type = type;
        this.number = number;
        this.description = description;
        this.date = date;
    }
}
