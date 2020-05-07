using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Bottom_Menu : MonoBehaviour
{
    public void LoadScene () {
      if (SceneManager.GetActiveScene().name != EventSystem.current.currentSelectedGameObject.name) {
        if (Application.CanStreamedLevelBeLoaded(EventSystem.current.currentSelectedGameObject.name)) {
          SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
        }
      }
    }
}
