using System.Collections.Generic;  // List ��� ���� �ʿ�
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public Text questionText;
    public Button[] choiceButtons;
    public Text progressText;

    private int[] characterScores = new int[3]; // ���ī, �����, ���� ����
    private int currentQuestion = 0;

    private string[] questions = {
        "ģ����� ���̰����� ���µ� �� �´ٸ�?",
        "������ �յΰ� ģ���� ���ʹ޶�� �ϸ�?",
        "���ο� ������Ʈ���� �ϰ� ���� ������?",
        "ģ���� ����� �о���´ٸ� ����?"
    };

    private string[,] choices = {
        { "�� �͵� �ų��� ����!", "�ǳ� �׸���ũ�� ����!", "����� ì��� �״�� ����!" },
        { "������� �ְ� ����!", "�� ���θ� ��ģ �� ���´�.", "���� �κи� �����ش�." },
        { "������ �Ǿ� �̲���!", "��ȹ�� �ð� �����Ѵ�.", "�����ϸ� ����ũ�� �����." },
        { "�������? ������, �� ����� ���ϰ� �־�! �� ������ �� ���̾�!", "������ �ٽ��� ���� ���� �����غ���? �ذ� ������� ã�ƺ���.", "�� �Ծ���? �� �Ծ����� ���� ���� �� ����! �ϴ� �� ä��� �������ڱ�!" }
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
        // 1. �ְ� ������ Ȯ��
        int maxScore = Mathf.Max(characterScores[0], characterScores[1], characterScores[2]);

        // 2. �ְ� ������ ���� ĳ���͵��� �ε����� ����Ʈ�� ����
        List<int> topIndexes = new List<int>();
        for (int i = 0; i < characterScores.Length; i++)
        {
            if (characterScores[i] == maxScore)
            {
                topIndexes.Add(i);
            }
        }

        // 3. �ְ� ������ ���� ĳ���Ͱ� �� �̻��̸�, �������� �� ���� ����
        if (topIndexes.Count > 1)
        {
            int randomIndex = Random.Range(0, topIndexes.Count);
            int winner = topIndexes[randomIndex];

            // ��) winner�� ������ ������ �ְ��� ĳ���͵��� ������ 1 ���缭
            //     ���� winner�� �ְ����� �ǵ��� ����
            //     (�Ǵ� winner�� ������ 1 �� ���̰ų� ��� ���ϴ� ������� ó�� ����)
            for (int i = 0; i < topIndexes.Count; i++)
            {
                if (i != randomIndex)
                {
                    characterScores[topIndexes[i]]--;
                    // �� ������ ������ ���̹Ƿ�, �ʿ信 �°� �����ϼ���.
                }
            }
        }

        // ���������� PlayerPrefs�� ������ ����
        PlayerPrefs.SetInt("MomokaScore", characterScores[0]);
        PlayerPrefs.SetInt("UmikoScore", characterScores[1]);
        PlayerPrefs.SetInt("HinaScore", characterScores[2]);

        // ��� ������ �̵�
        SceneManager.LoadScene("ResultScene");
    }
}
