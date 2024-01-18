using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DB_Manager
{
    public static string email;
<<<<<<< HEAD
    //public static int experience;
=======
>>>>>>> af324c0768c6974e98be73fe5f3f89bd0be906c9

    public static bool LoggedIn { get { return email != null; } }

    public static void LogOut()
    {
        email = null;
    }
}
