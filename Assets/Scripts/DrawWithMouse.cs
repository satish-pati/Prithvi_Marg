using UnityEngine;

public class DrawWithMouse3D : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;  
    public ParticleSystem particleSystem;
    private LineRenderer line;
    private Vector3 previousPosition;
    [SerializeField]
    private float min = 0.1f;
    [SerializeField]
    private float drawDistance = 10f;
    [SerializeField]
    private float CollisionRadius = 0.1f;
    private readonly string[] foodChain = { "Grass", "GrassHoper","Frog","Snake", "Eagle", "Mushroom" };
    private int[] mapped={0,0,0,0,0};
    public delegate void CorrectMap();
    public static event CorrectMap ScoreUpdate; 

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        previousPosition = Vector3.zero;
        line.positionCount=1;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 currentPosition = ray.GetPoint(drawDistance); 
            if (Vector3.Distance(currentPosition, previousPosition) > min)
            {
                if(previousPosition==Vector3.zero)
                {
                    line.SetPosition(0, currentPosition);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount-1, currentPosition);
                }
                previousPosition = currentPosition;
            }
        }
        else
        {
            Vector3 startPoint = line.GetPosition(0);
            Vector3 endPoint = line.GetPosition(line.positionCount-1);
            Collider[] startColliders = Physics.OverlapSphere(startPoint,CollisionRadius);
            Collider[] endColliders = Physics.OverlapSphere(endPoint, CollisionRadius);
            if (startColliders.Length > 0 && endColliders.Length > 0)
            {
                string startTag = startColliders[0].gameObject.tag; 
                string endTag = endColliders[(endColliders.Length)-1].gameObject.tag;
                if (IsFoodChainValid(startTag, endTag))
                {
                    if(ScoreUpdate!=null)
                    {
                        previousPosition = Vector3.zero;
                        line.positionCount=1;
                        ScoreUpdate();
                    }
                }
                else
                {
                    if(startTag!=endTag)
                    audioSource2.Play();
                    previousPosition = Vector3.zero;
                    line.positionCount=1;
                }
            }
            else
            {
                previousPosition = Vector3.zero;
                line.positionCount=1;
            }
        }
    }
    private bool IsFoodChainValid(string startTag, string endTag)
    {
        if (string.IsNullOrEmpty(startTag) || string.IsNullOrEmpty(endTag))
        {
            return false;
        }
        int startIndex = System.Array.IndexOf(foodChain, startTag);
        int endIndex = System.Array.IndexOf(foodChain, endTag);
        if (startIndex == -1)
        {
            return false;
        }
        if (endIndex == -1)
        {
            return false;
        }
        if (endIndex == startIndex + 1 && mapped[startIndex]==0)
        {
            mapped[startIndex]=1;
            StartCoroutine(PlayParticleSystem());
            audioSource1.Play();
            return true;
        }
        else
        {
            return false;
        }
    }
    private System.Collections.IEnumerator PlayParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
            yield return new WaitForSeconds(1f); // Wait for 1 second
            particleSystem.Stop();
        }
        else
        {
            Debug.LogWarning("Particle System is not assigned!");
        }
    }
}
