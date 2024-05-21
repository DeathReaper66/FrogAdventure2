using Photon.Pun;
using UnityEngine.SceneManagement;

public class GoToMainMenuButton : MonoBehaviourPunCallbacks
{
    public void GoToMainMain()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
    }
}
