using TMPro;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public GameManager gm;
    [SerializeField] private Material newMaterial;
    [SerializeField] private Material oldMaterial;
    public bool scoreMade = false;

    public Transform objectToMove;
    public int ring;

    public Vector3 targetPosition;
    public Quaternion targetRotation;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public float moveTime = 1f;
    private float elapsedTime;
    private bool isMoving = false;
    public bool transition1Complete = false;
    private bool transition1Started = false;

    private bool transition2Complete = false;
    private bool transition3Complete = false;

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / moveTime;

            // Position
            objectToMove.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Rotation
            objectToMove.rotation = Quaternion.Slerp(startRotation,targetRotation,t);

            if (t >= 1f)
            {
                objectToMove.position = targetPosition;
                objectToMove.rotation = targetRotation;
                isMoving = false;
                transition1Complete = true;
                ResetColor();
            }
        }


        if (gm.level == 2 && transition1Complete == false && transition1Started == false)
        {
            transition1Started = true;

            //moveto func
            if(ring == 1) { MoveTo(new Vector3(-0.18f, 12f, -0.47f), Quaternion.Euler(-87.795f, 0f, 0f)); }
            if(ring == 2) { MoveTo(new Vector3(-3.73f, 9f, -4.5f), Quaternion.Euler(15.944f, -6.564f, -49.14f)); }
            if(ring == 3) { MoveTo(new Vector3(4.26f, 8f, 3.62f), Quaternion.Euler(0f, 0f, -42.114f)); }
            if(ring == 4) { MoveTo(new Vector3(3.69f, 10f, -3.13f), Quaternion.Euler(-2.383f, -37.893f, 51.24f)); }
            if(ring == 5) { MoveTo(new Vector3(-3.01f, 9f, 3.56f), Quaternion.Euler(-117.437f, 0f, 0f)); }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (scoreMade == false)
        {
            if (other.CompareTag("CanInteract"))
            {
                MeshRenderer mr = GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.material = newMaterial;
                    scoreMade = true;
                    gm.ringsScored++;
                }
            }
        }
        
    }

    public void MoveTo(Vector3 newPosition, Quaternion newRotation)
    {
        startPosition = objectToMove.position;
        startRotation = objectToMove.rotation;

        targetPosition = newPosition;
        targetRotation = newRotation;

        elapsedTime = 0f;
        isMoving = true;
    }

    public void ResetColor()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.material = oldMaterial;
        }
    }
}
