using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomList : MonoBehaviourPunCallbacks
{
    public List<RoomButtons> roomButtons;
    private void Start()
    {
        StartCoroutine(JoinLobby());
        RoomOff();
    }
    void RefereshRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomButtons.Count; i++)
        {
            int x = i;
            roomButtons[i].button.onClick.RemoveAllListeners();
            if (roomList.Count > i)
            {
                roomButtons[i].button.onClick.AddListener(() =>
                {

                    LobbyManager._instance.StartRoom(roomList[x].Name);
                });
                roomButtons[i].text.gameObject.SetActive(true);
                roomButtons[i].button.gameObject.SetActive(true);
                roomButtons[i].text.text = roomList[i].Name;
            }
            else
            {
                roomButtons[i].text.gameObject.SetActive(false);
                roomButtons[i].button.gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator JoinLobby()
    {
        Leave();
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }

        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Status : " + "Connecting to server");
            PhotonNetwork.ConnectUsingSettings();
        }

        while (!PhotonNetwork.IsConnectedAndReady)
        {
            yield return null;
        }

        if (!PhotonNetwork.InLobby && PhotonNetwork.NetworkClientState != ClientState.JoiningLobby)
        {
            Debug.LogError("Status : " + $"Connecting to server) ,state ={PhotonNetwork.NetworkClientState}");
           
        }
        //while (!PhotonNetwork.InLobby)
        //{
        //    yield return null;
        //}
        
        //    Debug.LogError("Status : " + $"lobby joined");
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.LogError("connected to master");
        TypedLobbyInfo info = new();
        info.Type = LobbyType.Default;
        PhotonNetwork.JoinLobby(info);
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.LogError("Lobby Joined");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RefereshRoomList(roomList);

    }
    public void Leave()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.Disconnect();
    }
    void RoomOff()
    {
        foreach (var item in roomButtons)
        {
            item.button.gameObject.SetActive(false);
            item.text.gameObject.SetActive(false);
        }
    }
}
[System.Serializable]
public class RoomButtons
{
    public Button button;
    public TextMeshProUGUI text;
}
