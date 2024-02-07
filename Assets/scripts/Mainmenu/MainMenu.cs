using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void SinglePlayerGame()
   {
   
    SceneManager.LoadScene(1);
   }
   public void QuitGame()
   {
    Application.Quit();
   }

   public void SettingGame()
   {
      SceneManager.LoadScene(2);
   }
}
