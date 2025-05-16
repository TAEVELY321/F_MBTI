using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    public Text resultText;
    public Image resultImage; // 캐릭터 이미지
    public Image resultNameImage;
    public Sprite momokaSprite, umikoSprite, hinaSprite; // 캐릭터별 이미지
    public Sprite momokaNameSprite, umikoNameSprite, hinaNameSprite; // 캐릭터 이름 이미지

    void Start()
    {
        ShowResult();
        IncreaseParticipantCount();
    }

    void ShowResult()
    {
        int momokaScore = PlayerPrefs.GetInt("MomokaScore", 0);
        int umikoScore = PlayerPrefs.GetInt("UmikoScore", 0);
        int hinaScore = PlayerPrefs.GetInt("HinaScore", 0);

        string resultCharacter;
        Sprite selectedSprite;
        Sprite selectedNameSprite;

        if (momokaScore >= umikoScore && momokaScore >= hinaScore)
        {
            resultCharacter = "페어리 로제트";
            selectedSprite = momokaSprite;
            selectedNameSprite = momokaNameSprite;
        }
        else if (umikoScore >= momokaScore && umikoScore >= hinaScore)
        {
            resultCharacter = "페어리 아쿠아리우스";
            selectedSprite = umikoSprite;
            selectedNameSprite = umikoNameSprite;
        }
        else
        {
            resultCharacter = "페어리 솔라리스";
            selectedSprite = hinaSprite;
            selectedNameSprite = hinaNameSprite;
        }

        resultText.text = resultCharacter;
        resultImage.sprite = selectedSprite; // 선택된 캐릭터 이미지 설정
        resultNameImage.sprite = selectedNameSprite; // 선택된 캐릭터 이름 이미지 설정
    }

    void IncreaseParticipantCount()
    {
        int count = PlayerPrefs.GetInt("ParticipantCount", 0);
        count++;
        PlayerPrefs.SetInt("ParticipantCount", count);
        PlayerPrefs.Save();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
