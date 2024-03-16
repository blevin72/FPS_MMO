using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _GameManager;

    public static GameManager instance;

    internal MatchSettings matchSettings;

    internal int loadedCharacter; /*assigned a value in the CharacterScene_Manager script --> Load Character Button --> value = the slot
                                   (1-4) the player chooses from the Select Character Canvas*/

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one game manager in scene");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(_GameManager);
        }
    }

    #region PlayerTracking

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach(string _playerID in players.Keys)
    //    {
    //        GUILayout.Label(_playerID + " - " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}

    #endregion
}
