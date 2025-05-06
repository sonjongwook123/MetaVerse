using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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
    }
}
