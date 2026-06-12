using UnityEngine;

public class GameChoserMode : MonoBehaviour
{
    public void ChoseTimeMode()
    {
        GameManager.instance.MenuChoserTimeGameMode();
    }
    public void ChoseFreeMode()
    {
        GameManager.instance.MenuChoserFreeGameMode();
    }
    public void ChoseVersusMode()
    {
        GameManager.instance.MenuChoserVersusGameMode();
    }
}
