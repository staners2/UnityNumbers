using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Profile
{
    public Int32 id;
    public String login;
    public String password;
    public Country country;

    public Profile(Int32 id, String login, String password, Country country)
    {
        this.id = id;
        this.login = login;
        this.password = password;
        this.country = country;
    }
}
