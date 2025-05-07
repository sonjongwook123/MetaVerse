using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PetTaker : MonoBehaviourPunCallbacks
{
    public GameObject popupPrefab;


    public void OpenPopup()
    {
        if (photonView.IsMine)
        {
            popupPrefab.SetActive(true);
        }
    }

    public void TogglePetForAll()
    {
        photonView.RPC("TogglePetRPC", RpcTarget.All);
    }

    [PunRPC]
    void TogglePetRPC()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PhotonView playerPhotonView = playerObject.GetComponent<PhotonView>();
            Player playerComponent = playerObject.GetComponent<Player>();

            if (playerPhotonView.IsMine)
            {
                playerComponent.RequestTogglePet();
                return;
            }
        }
    }


}