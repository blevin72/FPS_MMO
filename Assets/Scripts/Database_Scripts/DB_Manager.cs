using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DB_Manager
{
    public static string email;

    public static int accountID;

    public static bool LoggedIn { get { return email != null; } }

    public static void LogOut()
    {
        email = null;
    }
}
