using TMPro;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public GameManager gm;
    [SerializeField] private Material newMaterial;
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
    private bool transition1Complete = false;
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
            }
        }


        if (gm.level == 2 && transition1Complete == false)
        {
            //moveto func
            if(ring == 1) { MoveTo(new Vector3(-0.47f, 6.73f, 16.02f), Quaternion.Euler(-87.795f, 0f, 0f)); }
            if(ring == 2) { MoveTo(new Vector3(- 5.6f, 1.59f, 13.69f), Quaternion.Euler(15.944f, -6.564f, -49.14f)); }
            if(ring == 3) { MoveTo(new Vector3(3.55f, 2.2f, 18.64f), Quaternion.Euler(0f, 0f, -42.114f)); }
            if(ring == 4) { MoveTo(new Vector3(-2.74f, 2.78f, 19.22f), Quaternion.Euler(-2.383f, -37.893f, 51.24f)); }
            if(ring == 5) { MoveTo(new Vector3(1.61f, 3.48f, 11.81f), Quaternion.Euler(-117.437f, 0f, 0f)); }
            
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
}
