using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject pet;
    public bool isPet;
    public Transform petPrefab;

    void Start()
    {
        isPet = false;
        pet = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gate"))
        {
            switch (other.name)
            {
                case "0":
                    {
                        SceneManager.LoadScene("MiniGame1");
                        break;
                    }
                case "1":
                    {
                        SceneManager.LoadScene("MiniGame2");
                        break;
                    }
                case "2":
                    {
                        break;
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

    public void TogglePet()
    {
        if (isPet == false)
        {
            isPet = true;
            pet = Instantiate(petPrefab, transform.position + new Vector3(0, -1f, 0), Quaternion.identity, transform).gameObject;
        }
        else
        {
            isPet = false;
            if (pet != null)
            {
                Destroy(pet);
                pet = null;
            }
        }
    }

}
