using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // 룸에 참가하면 플레이어 오브젝트 생성
        if (playerPrefab != null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player Prefab 또는 Spawn Point가 설정되지 않았습니다.");
        }
    }
}