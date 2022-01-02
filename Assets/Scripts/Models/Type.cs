using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Type
{
    public Int32 id;
    public String title;
    public String en_title;

    public Type(Int32 id, String title, String en_title)
    {
        this.id = id;
        this.title = title;
        this.en_title = en_title;
    }
}
