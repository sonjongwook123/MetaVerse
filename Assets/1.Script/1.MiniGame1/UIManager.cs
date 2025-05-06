using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Score;
    public Text ScoreList;
    public GameObject scorePanel;

    public void InitUi(int score)
    {
        scorePanel.SetActive(true);
        this.Score.text = score.ToString();

        ScoreDataListWrapper scoreDataListWrapper = new ScoreDataListWrapper();
        DateTime currentTime = DateTime.Now;

        if (scoreDataListWrapper.scoreDatas == null)
        {
            scoreDataListWrapper.scoreDatas = new List<ScoreData>();
        }
        string jsonString = PlayerPrefs.GetString("Scores");
        if (jsonString != null && jsonString != "")
        {
            ScoreDataListWrapper wrapper = JsonUtility.FromJson<ScoreDataListWrapper>(jsonString);
            scoreDataListWrapper = wrapper;
        }

        scoreDataListWrapper.scoreDatas.Add(new ScoreData(currentTime.ToString("yyyy-MM-dd HH:mm:ss"), score));

        jsonString = JsonUtility.ToJson(scoreDataListWrapper);
        // JSON 문자열을 PlayerPrefs에 저장합니다.
        PlayerPrefs.SetString("Scores", jsonString);
        PlayerPrefs.Save(); // 변경 사항을 디스크에 저장하는 것을 잊지 마세요!
        Debug.Log("점수 데이터 저장 완료: " + jsonString);

        ScoreList.text = ScoreListReturn();
    }

    public string ScoreListReturn()
    {
        List<ScoreData> loadedScores = new List<ScoreData>();
        string jsonString = PlayerPrefs.GetString("Scores");
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

}

[System.Serializable]
public class ScoreDataListWrapper
{
    public List<ScoreData> scoreDatas;
}

[System.Serializable]
public class ScoreData
{
    public string Time;
    public int Score;

    public ScoreData(string v1, int v2)
    {
        this.Time = v1;
        this.Score = v2;
    }
}