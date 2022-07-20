using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoCounter : MiniGame
{
    public GameObject PotatoContentLocation;
    public GameObject Potato_Prefab;

    private const int MINPOTATOCOUNT = 1;
    private const int MAXPOTATOCOUNT = 10;

    private static readonly int[] PosiblePotatoRotations = { 0, 90, 180, 270 };

    private int PotatoCount = 0;
    private float CorrectAnswer = 0f;
    private float MyAnswer = 0f;

    public void Awake() {
        TimeLeft = 10f;

        PotatoCount = Random.Range(MINPOTATOCOUNT, MAXPOTATOCOUNT);
        var PotatoContentLocationRect = PotatoContentLocation.GetComponent<RectTransform>().rect;
        GeneratePotatosOnScreen(PotatoCount, PotatoContentLocationRect.width, PotatoContentLocationRect.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStarted && !IsGameEnded) {
            TimeLeft = CountDown(TimeLeft);

            if (TimeLeft < 0) {
                IsGameEnded = true;
                EndGame(CalculateScore(TimeLeft, CorrectAnswer, MyAnswer), this);
            }
        }
    }

    public override void StartGame() {   
        IsGameStarted = true;
    }

    public override string Name {
        get {
            return MiniGameNames.PotatoCounter;
        }
    }

    private float CalculateScore(float timeLeft, float correctAnswer, float myAnswer) {
        return correctAnswer - myAnswer;
    }

    private void GeneratePotatosOnScreen(int potatoCount, float potatoContentWidth, float potatoContentHeighth) {
        for (int i = 0; i < potatoCount; i++) {
            var potatoX = Random.Range(0f, potatoContentWidth);
            var potatoY = Random.Range(0f, potatoContentHeighth);
            var potatoRotation = Random.Range(0, PosiblePotatoRotations.Length);

            GameObject newest_potato_object = (GameObject)Instantiate(Potato_Prefab, PotatoContentLocation.transform);
            var newest_potato_object_recttransform = newest_potato_object.GetComponent<RectTransform>();
            newest_potato_object_recttransform.anchoredPosition = new Vector2(potatoX, potatoY);
            newest_potato_object_recttransform.rotation = Quaternion.Euler(0, 0, PosiblePotatoRotations[potatoRotation]);
        }
    }
}
