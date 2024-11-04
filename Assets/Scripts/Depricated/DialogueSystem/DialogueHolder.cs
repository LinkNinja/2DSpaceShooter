using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        private LevelChanger changeLevel;
        private void Awake()
        {
            StartCoroutine(dialogueSequence());
            //changeLevel = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        }

        private IEnumerator dialogueSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            
            gameObject.SetActive(false);
            //changeLevel.FadeToNextLevel();

        }


        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        
        }




    }

}