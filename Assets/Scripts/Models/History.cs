using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class History
{
    public int id;
    public Fact fact;
    public UserProfile userProfile;

    public History(int id, UserProfile userProfile, Fact fact)
    {
        this.id = id;
        this.userProfile = userProfile;
        this.fact = fact;
    }
}
