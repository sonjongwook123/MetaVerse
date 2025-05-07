using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ColorStore : MonoBehaviourPunCallbacks
{
    public List<Color> colors;
    public GameObject popup;

    public void OpenPopup()
    {
        if (photonView.IsMine)
        {
            popup.SetActive(true);
        }
    }

    public void SelectColorForAll(int colorIndex)
    {
        photonView.RPC("SetPlayerColorRPC", RpcTarget.All, colorIndex);
    }

    [PunRPC]
    void SetPlayerColorRPC(int colorIndex)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PhotonView playerPhotonView = playerObject.GetComponent<PhotonView>();
            SpriteRenderer spriteRenderer = playerObject.GetComponent<SpriteRenderer>();

            if (playerPhotonView != null && playerPhotonView.IsMine && spriteRenderer != null && colorIndex >= 0 && colorIndex < colors.Count)
            {
                spriteRenderer.color = colors[colorIndex];
                return;
            }
        }
    }

}
