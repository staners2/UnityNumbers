using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class History
{
    public Int32 id;
    public Fact fact;
    public UserProfile userProfile;

    public History(Int32 id, UserProfile userProfile, Fact fact)
    {
        this.id = id;
        this.userProfile = userProfile;
        this.fact = fact;
    }
}
