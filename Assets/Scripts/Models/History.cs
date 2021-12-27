using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class History
{
    public Int32 id;
    public Profile profile;
    public Type type;
    public Int32 number;
    public String description;
    public String date;

    public History(Int32 id, Profile profile, Type type, Int32 number, String description, String date)
    {
        this.id = id;
        this.profile = profile;
        this.type = type;
        this.number = number;
        this.description = description;
        this.date = date;
    }
}
