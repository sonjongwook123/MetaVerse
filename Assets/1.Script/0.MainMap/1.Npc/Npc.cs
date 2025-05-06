using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public GameObject popupTalk;
    public Text talkText;
    public InputField selectNum;
    public List<TalkData> talks;
    public int currentMessage;
    public Button nextButton;

    void Start()
    {
        currentMessage = 0;
        nextButton.onClick.AddListener(Next);
    }

    public void Talk()
    {
        popupTalk.SetActive(true);
        if (currentMessage > talks.Count)
        {
            popupTalk.SetActive(false);
            currentMessage = 0;
            selectNum.gameObject.SetActive(false);
            return;
        }

        if (talks[currentMessage].IsSub == false)
        {
            selectNum.gameObject.SetActive(false);
            talkText.text = talks[currentMessage].Message;
        }
        else
        {
            selectNum.gameObject.SetActive(true);
            string @message = "";
            foreach (SubTalkData item in talks[currentMessage].SubTalks)
            {
                @message += item.Message + "\n";
                Debug.Log(item.Message);
            }
            talkText.text = @message;
        }
    }

    public void Next()
    {
        if (talks[currentMessage].IsSub == false)
        {
            currentMessage = talks[currentMessage].NextCode;
            Talk();
        }
        else
        {
            int selectedNum = 0;
            if (int.TryParse(selectNum.text, out selectedNum) == true)
            {
                if (selectedNum > 0 && selectedNum <= talks[currentMessage].SubTalks.Count)
                {
                    currentMessage = talks[currentMessage].SubTalks[selectedNum - 1].NextCode;
                    Talk();
                }
            }
        }
    }

}

[System.Serializable]
public class TalkData
{
    public int Code;
    public int NextCode;
    public bool IsSub;
    public string Message;
    public List<SubTalkData> SubTalks;
}

[System.Serializable]
public class SubTalkData
{
    public int Code;
    public int NextCode;
    public string Message;
}
