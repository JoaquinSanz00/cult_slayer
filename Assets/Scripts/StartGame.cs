using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Animator blackPanel;

    public void LoadGameScene()
    {
        StartCoroutine(LoadGameSceneCR());
    }

    IEnumerator LoadGameSceneCR()
    {
        blackPanel.SetBool("start", true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainScene");
    }
}
