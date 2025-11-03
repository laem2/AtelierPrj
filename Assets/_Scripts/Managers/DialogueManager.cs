using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public TMP_Text dialogueText;
    public GameObject robotPanel;

    [Header("Glow Objects")]
    public GameObject[] inputObjects;
    public GameObject[] dropTargets;
    public GameObject node;
    public GameObject output;
    public GameObject predictButton;

    private Queue<string> dialogueQueue = new Queue<string>();
    private bool isTyping = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        robotPanel.SetActive(true);
        ShowDialogue("Hello! Let's start by dragging the inputs into their correct places.");
        SetGlow(inputObjects, true);
        SetGlow(dropTargets, true);
    }

    public void OnInputsPlaced()
    {
        ShowDialogue("Good job! Now, link each input to the node and the node to the output.");
        SetGlow(inputObjects, false);
        SetGlow(dropTargets, false);
        SetGlow(new GameObject[] { node }, true);
    }

    public void OnOutputConnected()
    {
        ShowDialogue("Perfect! Now press the Predict button to see the results.");
        SetGlow(new GameObject[] { output }, false);
        SetGlow(new GameObject[] { predictButton }, true);
    }

    public void OnPredictionShown()
    {
        StartCoroutine(HandlePredictionDialogue());
    }

    private IEnumerator HandlePredictionDialogue()
    {
        yield return StartCoroutine(TypeDialogue("Oh no... most predictions are wrong... ??"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(TypeDialogue("Hmm... I think I know a tip! Let's adjust the weights!"));
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Weights");
    }

    // ?? Core dialogue typing system (with queue)
    public void ShowDialogue(string text)
    {
        dialogueQueue.Enqueue(text);
        if (!isTyping)
            StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        while (dialogueQueue.Count > 0)
        {
            string nextLine = dialogueQueue.Dequeue();
            yield return StartCoroutine(TypeDialogue(nextLine));
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator TypeDialogue(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
    }

    private void SetGlow(GameObject[] objs, bool enable)
    {
        foreach (var obj in objs)
        {
            if (obj != null && obj.TryGetComponent(out SpriteRenderer sr))
                sr.material.SetFloat("_GlowIntensity", enable ? 1f : 0f);
        }
    }
}


