using UnityEngine;
using System.Collections;


public class StatesGui : MonoBehaviour
{
    public Rect GuiOffset = new Rect(250,0,300,300);
    public bool DontDestroy = true;
    public bool Time;
    public bool DetailedConnection;
    public bool Server;
    public bool AppVersion;
    public bool UserId;
    public bool Room;
    public bool RoomProps;
    public bool Player;
    public bool PlayerProps;
    public bool Others;
    public bool Buttons;
    public bool ExpectedUsers;

    private Rect GuiRect = new Rect();
    private static GameObject Instance;

    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        if (DontDestroy)
        {
            Instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnGUI()
	{
        Rect GuiOffsetRuntime = new Rect(this.GuiOffset);

        if (GuiOffsetRuntime.x < 0)
        {
            GuiOffsetRuntime.x = Screen.width - GuiOffsetRuntime.width;
        }
        GuiRect.xMin = GuiOffsetRuntime.x;
        GuiRect.yMin = GuiOffsetRuntime.y;
        GuiRect.xMax = GuiOffsetRuntime.x + GuiOffsetRuntime.width;
        GuiRect.yMax = GuiOffsetRuntime.y + GuiOffsetRuntime.height;
        GUILayout.BeginArea(GuiRect);

        GUILayout.BeginHorizontal();
        if (Time)
        {
            GUILayout.Label(PhotonNetwork.ServerTimestamp.ToString("F3"));
        }

        if (Server)
        {
            GUILayout.Label(PhotonNetwork.ServerAddress + " " + PhotonNetwork.Server);
        }
        if (DetailedConnection)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }
        if (AppVersion)
        {
            GUILayout.Label(PhotonNetwork.networkingPeer.AppVersion);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (UserId)
        {
            GUILayout.Label("UID: " + ((PhotonNetwork.AuthValues != null) ? PhotonNetwork.AuthValues.UserId : "no UserId"));
        }
        GUILayout.EndHorizontal();

        if (Room)
        {
            if (PhotonNetwork.inRoom)
            {
                GUILayout.Label(this.RoomProps ? PhotonNetwork.room.ToStringFull() : PhotonNetwork.room.ToString());
                GUILayout.Label((PhotonNetwork.room.serverSideMasterClient ? "Server Side Master Client" : "Client Side Master Client"));
            }
            else
            {
                GUILayout.Label("not in room");
            }
        }


        
        if (Player)
        {
            GUILayout.Label(PlayerToString(PhotonNetwork.player));
        }
        if (Others)
        {
            foreach (PhotonPlayer player in PhotonNetwork.otherPlayers)
            {
                GUILayout.Label(PlayerToString(player));
            }
        }
        if (ExpectedUsers)
        {
            if (PhotonNetwork.inRoom)
            {
                int countExpected = (PhotonNetwork.room.ExpectedUsers != null) ? PhotonNetwork.room.ExpectedUsers.Length : 0;

				GUILayout.Label("Expected: " + countExpected + " " + 
				               ( (PhotonNetwork.room.ExpectedUsers != null) ? string.Join(",",PhotonNetwork.room.ExpectedUsers) : "" )
				                );

            }
        }


        if (Buttons)
        {
            if (!PhotonNetwork.connected && GUILayout.Button("Connect"))
            {
                PhotonNetwork.ConnectUsingSettings(null);
            }
            GUILayout.BeginHorizontal();
            if (PhotonNetwork.connected && GUILayout.Button("Disconnect"))
            {
                PhotonNetwork.Disconnect();
            }
            if (PhotonNetwork.connected && GUILayout.Button("Close Socket"))
            {
                PhotonNetwork.networkingPeer.StopThread();
            }
            GUILayout.EndHorizontal();
            if (PhotonNetwork.connected && PhotonNetwork.inRoom && GUILayout.Button("Leave"))
            {
                PhotonNetwork.LeaveRoom();
            }
            if (PhotonNetwork.connected && !PhotonNetwork.inRoom && GUILayout.Button("Join Random"))
            {
                PhotonNetwork.JoinRandomRoom();
            }
            if (PhotonNetwork.connected && !PhotonNetwork.inRoom && GUILayout.Button("Create Room"))
            {
                PhotonNetwork.CreateRoom(null);
            }
        }

	    GUILayout.EndArea();
	}

    private string PlayerToString(PhotonPlayer player)
    {
        if (PhotonNetwork.networkingPeer == null)
        {
            Debug.LogError("nwp is null");
            return "";
        }
		return string.Format("#{0:00} '{1}'{5} {4}{2} {3} {6}", player.ID, player.NickName, player.IsMasterClient ? "(master)" : "", this.PlayerProps ? player.CustomProperties.ToStringFull() : "", (PhotonNetwork.player.ID==player.ID)?"(you)":"", player.UserId,player.IsInactive?" / Is Inactive":"");
    }
}
