using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceManager : MonoBehaviour {

    KeywordRecognizer keywoardRec;
    delegate void KeywordAction();
    Dictionary<string, KeywordAction> collection;
    /*
	void Start () {
        collection = new Dictionary<string, KeywordAction>;
        collection.Add("Show", SendMessage("show"));
        collection.Add("Hide", SendMessage("Hide"));

        keywoardRec = new KeywordRecognizer(collection.Keys.ToArray());
        keywoardRec.OnPhraseRecognized += keywoardRec_OnPhaseRecognized;
        keywoardRec.Start();		
	}
	
	private void keywoardRec_OnPhaseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywodAction;
        if (collection.TryGetValue(args.text, out keywodAction))
        {
            keywodAction.Invoke(); 
        }
    }
    */
  
}
