using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    public Text resultText;
    public Image resultImage; // ĳ���� �̹���
    public Image resultNameImage;
    public Sprite momokaSprite, umikoSprite, hinaSprite; // ĳ���ͺ� �̹���
    public Sprite momokaNameSprite, umikoNameSprite, hinaNameSprite; // ĳ���� �̸� �̹���

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
            resultCharacter = "�� ����Ʈ";
            selectedSprite = momokaSprite;
            selectedNameSprite = momokaNameSprite;
        }
        else if (umikoScore >= momokaScore && umikoScore >= hinaScore)
        {
            resultCharacter = "�� ����Ƹ��콺";
            selectedSprite = umikoSprite;
            selectedNameSprite = umikoNameSprite;
        }
        else
        {
            resultCharacter = "�� �ֶ󸮽�";
            selectedSprite = hinaSprite;
            selectedNameSprite = hinaNameSprite;
        }

        resultText.text = resultCharacter;
        resultImage.sprite = selectedSprite; // ���õ� ĳ���� �̹��� ����
        resultNameImage.sprite = selectedNameSprite; // ���õ� ĳ���� �̸� �̹��� ����
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
