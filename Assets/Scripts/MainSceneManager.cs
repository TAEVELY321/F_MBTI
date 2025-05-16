using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class MainSceneManager : MonoBehaviour
{
    public Image main;
    public Text with;
    public Text mbti;
    public Text whour;
    public float fadeDuration = 1.5f;

    public Text participantText;

    void Start()
    {
        StartCoroutine(FadeInUI());
        UpdateParticipantCount();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MBTIScene");
    }

    private void UpdateParticipantCount()
    {
        int participantCount = PlayerPrefs.GetInt("ParticipantCount", 0);
        participantText.text = $"총 참여인원 : {participantCount}명";
    }

    private IEnumerator FadeInUI()
    {
        float elapsed = 0f;

        // 초기 색상 저장
        Color mainColor = main.color;
        Color withColor = with.color;
        Color mbtiColor = mbti.color;
        Color whourColor = whour.color;

        // 초기 알파 0
        mainColor.a = 0f;
        withColor.a = 0f;
        mbtiColor.a = 0f;
        whourColor.a = 0f;

        main.color = mainColor;
        with.color = withColor;
        mbti.color = mbtiColor;
        whour.color = whourColor;

        // 점점 알파 증가
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            mainColor.a = alpha;
            withColor.a = alpha;
            mbtiColor.a = alpha;
            whourColor.a = alpha;

            main.color = mainColor;
            with.color = withColor;
            mbti.color = mbtiColor;
            whour.color = whourColor;

            yield return null;
        }
    }


}
