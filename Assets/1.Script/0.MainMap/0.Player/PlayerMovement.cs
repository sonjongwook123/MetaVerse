using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f; // 이동 속도 (Inspector 창에서 조절 가능)
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트

    void Start()
    {
        // SpriteRenderer 컴포넌트를 찾아서 할당합니다.
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
            enabled = false;
        }

        if (!photonView.IsMine)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            float moveDistance = 0;

            if (GetComponent<Player>() != null && GetComponent<Player>().isPet == true)
            {
                moveDistance = moveSpeed * 2 * Time.deltaTime;
            }
            else
            {
                moveDistance = moveSpeed * Time.deltaTime;
            }
            transform.Translate(movement * moveDistance);

            photonView.RPC("SetFlipXRPC", RpcTarget.All, horizontalInput < 0, GetComponent<Player>().isPet == true);
            photonView.RPC("UpdatePositionRPC", RpcTarget.Others, transform.position);
        }
    }

    [PunRPC]
    void SetFlipXRPC(bool flipX, bool flipPetX)
    {
        spriteRenderer.flipX = flipX;
        if (GetComponent<Player>() != null && GetComponent<Player>().isPet == true && GetComponentInChildren<SpriteRenderer>() != null)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = flipPetX;
        }
    }

    [PunRPC]
    void UpdatePositionRPC(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

}