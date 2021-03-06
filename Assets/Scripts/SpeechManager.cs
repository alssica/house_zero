﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	// Use this for initialization
	void Start () {

        //open close
        keywords.Add("Open", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnOpen");
            }
        });

        keywords.Add("Close", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnClose");
            }
        });

        //controls the width of opening
        keywords.Add("Open A Quarter", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnQuarter");
            }
        });

        keywords.Add("Half", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnHalf");
            }
        });

        keywords.Add("Open Three Quarters", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnThreeQuarters");
            }
        });

        //calls for UI panel to appear
        keywords.Add("Panel", () =>
        {
            var UIobj = TriggerUI.TriggerObj;
            UIobj.SendMessage("OnShow");
        });

        keywords.Add("Hide", () =>
        {
            var UIobj = TriggerUI.TriggerObj;
            UIobj.SendMessage("OnHide");

            var UIpop = GameObject.Find("UIpop");
            UIpop.SendMessage("OnHide");

            //var UIpopout = UIPop.TriggerObj;
            //UIpopout.SendMessage("OnHide");
        });


        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }
	
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
