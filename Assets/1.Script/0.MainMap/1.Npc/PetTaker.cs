using UnityEngine;

public class PetTaker : MonoBehaviour
{
    public GameObject popupPrefab;


    public void OpenPopup()
    {
        popupPrefab.SetActive(true);
    }

    public void TogglePet()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().TogglePet();
    }


}