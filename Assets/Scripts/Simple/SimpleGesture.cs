using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class SimpleGesture : MonoBehaviour {

    public static SimpleGesture Instance;
    public bool isManipulating;

    private GestureRecognizer Recognizer;

    void Awake () {
		if (Instance == null)
        {
            Instance = this;
        }

        isManipulating = false;
        Recognizer = new GestureRecognizer();
        //click
        Recognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

        //the event to listen to
        Recognizer.ManipulationStartedEvent += Started;
        Recognizer.ManipulationUpdatedEvent += Updated;
        Recognizer.ManipulationCanceledEvent += Ended;
        Recognizer.ManipulationCompletedEvent += Ended;

        Recognizer.StartCapturingGestures();
    }

    //unsubscribe
    private void OnDestroy()
    {
        Recognizer.ManipulationStartedEvent -= Started;
        Recognizer.ManipulationUpdatedEvent -= Updated;
        Recognizer.ManipulationCanceledEvent -= Ended;
        Recognizer.ManipulationCompletedEvent -= Ended;
    }

    private void Started(InteractionSourceKind source, Vector3 position, Ray headRay)
    {
        if (SimpleCursor.Instance.FocusedObj != null)
        {
            isManipulating = true;
            SimpleCursor.Instance.FocusedObj.SendMessage("StartManipulation", position);
        }
    }

    private void Updated(InteractionSourceKind source, Vector3 position, Ray headRay)
    {
        if (SimpleCursor.Instance.FocusedObj != null)
        {
            isManipulating = true;
            SimpleCursor.Instance.FocusedObj.SendMessage("UpdateManipulation", position);
        }
    }

    private void Ended(InteractionSourceKind source, Vector3 position, Ray headRay)
    {
        isManipulating = false;
    }

    public void Stop()
    {
        Recognizer.StopCapturingGestures();
    }
}
