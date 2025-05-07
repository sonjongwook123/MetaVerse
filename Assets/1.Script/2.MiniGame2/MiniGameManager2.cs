using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class MiniGameManager2 : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public bool isGameOver;
    public float time;

    public Text timerText;

    public Text result;
    public Text Score;
    public Text ScoreList;
    public GameObject scorePanel;


    void Start()
    {
        isGameOver = false;
        time = 20;

        StartCoroutine(timerCor());
    }

    void Update()
    {
        if (Player.transform.position.y > 59 && isGameOver == false)
        {
            isGameOver = true;
            GameOver(true);
        }
    }

    public void GameOver(bool isSuccess)
    {
        scorePanel.SetActive(true);
        this.Score.text = time.ToString();

        if (isSuccess == true)
        {
            ScoreDataListWrapper scoreDataListWrapper = new ScoreDataListWrapper();
            DateTime currentTime = DateTime.Now;

            if (scoreDataListWrapper.scoreDatas == null)
            {
                scoreDataListWrapper.scoreDatas = new List<ScoreData>();
            }
            string jsonString = PlayerPrefs.GetString("Scores2");
            if (jsonString != null && jsonString != "")
            {
                ScoreDataListWrapper wrapper = JsonUtility.FromJson<ScoreDataListWrapper>(jsonString);
                scoreDataListWrapper = wrapper;
            }

            scoreDataListWrapper.scoreDatas.Add(new ScoreData(currentTime.ToString("yyyy-MM-dd HH:mm:ss"), (int)time));

            jsonString = JsonUtility.ToJson(scoreDataListWrapper);
            // JSON 문자열을 PlayerPrefs에 저장합니다.
            PlayerPrefs.SetString("Scores2", jsonString);
            PlayerPrefs.Save(); // 변경 사항을 디스크에 저장하는 것을 잊지 마세요!
            Debug.Log("점수 데이터 저장 완료: " + jsonString);

            ScoreList.text = ScoreListReturn();

            result.text = "성공!";
        }
        else
        {
            result.text = "실패";
            this.Score.text = "";
        }
    }

    IEnumerator timerCor()
    {
        while (isGameOver == false)
        {
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
            timerText.text = time.ToString() + "초 남음";

            if (time <= 0)
            {
                isGameOver = true;
                break;
            }
        }
        if (time <= 0)
        {
            GameOver(false);
        }
    }

    public string ScoreListReturn()
    {
        List<ScoreData> loadedScores = new List<ScoreData>();
        string jsonString = PlayerPrefs.GetString("Scores2");
        string scoreStringResult = "저장된 점수가 없습니다.";

        if (!string.IsNullOrEmpty(jsonString))
        {
            ScoreDataListWrapper wrapper = JsonUtility.FromJson<ScoreDataListWrapper>(jsonString);

            if (wrapper != null && wrapper.scoreDatas != null)
            {
                loadedScores = wrapper.scoreDatas;
                // 점수 오름차순으로 정렬하고 상위 maxDisplayCount 개수만 유지합니다.
                loadedScores = loadedScores.OrderByDescending(data => data.Score).Take(10).ToList();

                if (loadedScores.Count > 0)
                {
                    scoreStringResult = "오름차순 점수:\n";
                    for (int i = 0; i < loadedScores.Count; i++)
                    {
                        scoreStringResult += $"{i + 1}. {loadedScores[i].Score} (저장 시간: {loadedScores[i].Time})\n";
                    }
                }
                else
                {
                    scoreStringResult = "저장된 점수가 없습니다.";
                }
                Debug.Log("점수 데이터 로드 및 정렬 완료 (오름차순, 상위 " + 10 + "개): " + scoreStringResult);
            }
            else
            {
                Debug.Log("저장된 점수 데이터가 없거나 형식이 잘못되었습니다 (JsonUtility).");
            }
        }
        else
        {
            Debug.Log("저장된 점수 데이터가 없습니다 (JsonUtility).");
        }

        return scoreStringResult;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBackToMain()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Main");
    }

}
