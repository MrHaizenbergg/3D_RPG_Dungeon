using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject player;
    public GameObject Player {  get { return player; } }

    [SerializeField] private GameObject chest;
    public GameObject Chest { get { return chest; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogWarning("PlayerManager more one found");
    }


    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
