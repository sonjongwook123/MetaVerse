using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamaManager : MonoBehaviour
{
    public static MainGamaManager Instance { get; private set; } // 전역 접근용 인스턴스


    void Awake()
    {
        // 인스턴스가 없으면 자신을 할당
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
