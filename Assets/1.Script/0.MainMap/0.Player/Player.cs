using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
public class Player : MonoBehaviourPunCallbacks
{
    public GameObject pet;
    public bool isPet;
    public Transform petPrefab;

    void Start()
    {
        if (photonView.IsMine)
        {
            isPet = false;
            pet = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if (other.CompareTag("Gate"))
            {
                switch (other.name)
                {
                    case "0":
                        {
                            PhotonNetwork.LeaveRoom();
                            SceneManager.LoadScene("MiniGame1");
                            break;
                        }
                    case "1":
                        {
                            PhotonNetwork.LeaveRoom();
                            SceneManager.LoadScene("MiniGame2");
                            break;
                        }
                    case "2":
                        {
                            break;
                        }
                }
            }
        }

        if (other.CompareTag("Npc"))
        {
            other.GetComponent<Npc>().Talk();
        }

        if (other.CompareTag("PetTaker"))
        {
            other.GetComponent<PetTaker>().OpenPopup();
        }

        if (other.CompareTag("ColorStore"))
        {
            other.GetComponent<ColorStore>().OpenPopup();
        }
    }


    [PunRPC]
    public void TogglePetRPC()
    {
        isPet = !isPet;
        if (isPet)
        {
            if (photonView.IsMine)
            {
                pet = Instantiate(petPrefab, transform.position + new Vector3(0, -1f, 0), Quaternion.identity, transform).gameObject;
            }
        }
        else
        {
            if (photonView.IsMine && pet != null)
            {
                Destroy(pet);
                pet = null;
            }
        }
    }

    public void RequestTogglePet()
    {
        photonView.RPC("TogglePetRPC", RpcTarget.All);
    }

    public override void OnJoinedRoom()
    {
        foreach (Photon.Realtime.Player otherPlayer in PhotonNetwork.PlayerListOthers)
        {
            photonView.RPC("RequestOtherPlayerPetStateAndToggleRPC", otherPlayer);
        }
    }

    [PunRPC]
    void RequestOtherPlayerPetStateAndToggleRPC()
    {
        photonView.RPC("HandleOtherPlayerPetToggleRPC", RpcTarget.All, photonView.ViewID, isPet);
    }

    [PunRPC]
    void HandleOtherPlayerPetToggleRPC(int playerViewID, bool otherPlayerHasPet)
    {
        if (photonView.IsMine && playerViewID != photonView.ViewID)
        {
            photonView.RPC("TogglePetRPC", RpcTarget.All);
        }
    }

}
