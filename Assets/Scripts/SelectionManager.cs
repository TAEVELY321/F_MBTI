using System.Collections.Generic;  // List 사용 위해 필요
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public Text questionText;
    public Button[] choiceButtons;
    public Text progressText;

    private int[] characterScores = new int[3]; // 모모카, 우미코, 히나 점수
    private int currentQuestion = 0;

    private string[] questions = {
        "친구들과 놀이공원에 갔는데 비가 온다면?",
        "시험을 앞두고 친구가 도와달라고 하면?",
        "새로운 프로젝트에서 하고 싶은 역할은?",
        "친구가 고민을 털어놓는다면 나는?"
    };

    private string[,] choices = {
        { "비가 와도 신나게 즐긴다!", "실내 테마파크로 변경!", "우산을 챙기고 그대로 진행!" },
        { "도움부터 주고 본다!", "내 공부를 마친 후 돕는다.", "급한 부분만 도와준다." },
        { "리더가 되어 이끈다!", "기획을 맡고 조율한다.", "조율하며 팀워크를 맞춘다." },
        { "힘들었지? 괜찮아, 넌 충분히 잘하고 있어! 난 언제나 네 편이야!", "문제의 핵심이 뭔지 같이 정리해볼까? 해결 방법부터 찾아보자.", "밥 먹었어? 안 먹었으면 나랑 같이 밥 먹자! 일단 배 채우고 생각하자구!" }
    };

    void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (currentQuestion >= questions.Length)
        {
            GoToResultScene();
            return;
        }

        questionText.text = questions[currentQuestion];
        progressText.text = $"{currentQuestion + 1} / {questions.Length}";

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int choiceIndex = i;
            choiceButtons[i].GetComponentInChildren<Text>().text = choices[currentQuestion, i];
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => SelectChoice(choiceIndex));
        }
    }

    void SelectChoice(int index)
    {
        characterScores[index]++;
        currentQuestion++;
        DisplayQuestion();
    }

    void GoToResultScene()
    {
        // 1. 최고 점수를 확인
        int maxScore = Mathf.Max(characterScores[0], characterScores[1], characterScores[2]);

        // 2. 최고 점수를 받은 캐릭터들의 인덱스를 리스트로 저장
        List<int> topIndexes = new List<int>();
        for (int i = 0; i < characterScores.Length; i++)
        {
            if (characterScores[i] == maxScore)
            {
                topIndexes.Add(i);
            }
        }

        // 3. 최고 점수를 받은 캐릭터가 둘 이상이면, 무작위로 한 명을 결정
        if (topIndexes.Count > 1)
        {
            int randomIndex = Random.Range(0, topIndexes.Count);
            int winner = topIndexes[randomIndex];

            // 예) winner를 제외한 나머지 최고점 캐릭터들은 점수를 1 낮춰서
            //     오직 winner만 최고점이 되도록 조정
            //     (또는 winner의 점수를 1 더 높이거나 등등 원하는 방식으로 처리 가능)
            for (int i = 0; i < topIndexes.Count; i++)
            {
                if (i != randomIndex)
                {
                    characterScores[topIndexes[i]]--;
                    // 이 로직은 예시일 뿐이므로, 필요에 맞게 조정하세요.
                }
            }
        }

        // 최종적으로 PlayerPrefs에 점수를 저장
        PlayerPrefs.SetInt("MomokaScore", characterScores[0]);
        PlayerPrefs.SetInt("UmikoScore", characterScores[1]);
        PlayerPrefs.SetInt("HinaScore", characterScores[2]);

        // 결과 씬으로 이동
        SceneManager.LoadScene("ResultScene");
    }
}
