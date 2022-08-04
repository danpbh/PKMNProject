using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EventAutomation : MonoBehaviour
{
    [MenuItem("Assets/AddAnimationEvent")]
    static void SetAnimationEvent()
    {
        foreach (var animclip in Selection.GetFiltered<AnimationClip>(SelectionMode.Assets))
        {
            if (animclip != null)
            {
                AnimationEvent evt = new AnimationEvent();
                evt.functionName = "AnimAtStart";
                evt.time = 0;
                evt.messageOptions = SendMessageOptions.DontRequireReceiver;
                AnimationEvent[] allevents = AnimationUtility.GetAnimationEvents(animclip);
                Debug.Log("Animation Events: " + allevents.Length);
                if (allevents.Count(x => x.functionName == "AnimAtStart") != 0) continue;
                Debug.Log("Adding Animation Event");
                IEnumerable<AnimationEvent> test = allevents.Append(evt);
                AnimationUtility.SetAnimationEvents(animclip, test.ToArray());
            }
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/AddAnimationEvent", true)]
    private static bool SetAnimationEventValidation()
    {
        // This returns true when the selected object is a Variable (the menu item will be disabled otherwise).
        return Selection.GetFiltered<AnimationClip>(SelectionMode.Assets).Length > 0;
    }
}
