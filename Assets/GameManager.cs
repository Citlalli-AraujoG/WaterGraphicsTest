using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ring[] rings;
    private int numRings = 5;//number of rings scored in this level
    private float timer = 0;
    public int level = 1;
    public int ringsScored = 0;
    public int totalBallsThrown = 0;
    public float accuracy = 0;

    void GameResults()
    {
        accuracy = ringsScored * (100 / totalBallsThrown);
    }

    void Update()
    {
        timer += Time.deltaTime;

        int ringCount = 0;
        for(int i = 0; i < rings.Length; i++)
        {
            if(rings[i].scoreMade == true)
            {
                ringCount++;
            }
        }

        if(ringCount == numRings)
        {
            level += 1;
            if (level == 2)
            {
                PlayLevel2();
            }
            else if (level == 3)
            {
                PlayLevel3();
            }
            else if  (level == 4)
            {
                PlayLevel4();
            }
            else
            {
                level = 0;//end of game
            }
        }
    }

    void PlayLevel2()
    {

    }
    void PlayLevel3()
    {

    }
    void PlayLevel4()
    {

    }

}
