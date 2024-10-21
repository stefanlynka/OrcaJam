using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public String nextLevelName;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(nextLevelName);
            SwooshAudio.Instance.PlaySwoosh();
        }
    }


}
