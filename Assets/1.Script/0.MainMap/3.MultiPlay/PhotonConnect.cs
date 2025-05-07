using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    public int roomNumber;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon 서버에 연결 시도...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 연결됨");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 참가함");
        PhotonNetwork.JoinOrCreateRoom("MyRoom" + roomNumber, new RoomOptions(), TypedLobby.Default); // "MyRoom" 이름의 룸 참가 또는 생성
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸에 참가함");
        Debug.Log("현재 룸 인원 수: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("룸 참가 실패: " + message);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " 님이 룸에 참가했습니다.");
        Debug.Log("현재 룸 인원 수: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " 님이 룸에서 나갔습니다.");
        Debug.Log("현재 룸 인원 수: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

}