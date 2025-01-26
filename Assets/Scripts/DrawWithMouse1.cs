using UnityEngine;

public class DrawWithMouse3D1 : MonoBehaviour
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
    private readonly string[] entities = { "Grass", "Mouse", "Rabbit", "Deer", "Snake", "Eagle", "Cat", "Tiger", "Fox" };
    private readonly string[][] foodWeb = {
        new string[] { "Mouse", "Rabbit", "Deer" },        
        new string[] { "Snake","Cat" },           
        new string[] { "Fox", "Cat"},         
        new string[] { "Tiger", "Fox" },      
        new string[] { "Eagle"},                
        new string[] { },                                     
        new string[] { "Tiger" },                            
        new string[] { },                                    
        new string[] {  "Tiger" }                                     
    };
    private int[,] mappedConnections;
    public delegate void CorrectMap1();
    public static event CorrectMap1 ScoreUpdate1; 

    private void Start()
    {
        int n = entities.Length;
        mappedConnections = new int[n, n];
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
            Collider[] startColliders1 = Physics.OverlapSphere(startPoint,CollisionRadius);
            Collider[] endColliders1 = Physics.OverlapSphere(endPoint, CollisionRadius);
            if (startColliders1.Length > 0 && endColliders1.Length > 0)
            {
                string startTag = startColliders1[0].gameObject.tag; 
                string endTag = endColliders1[(endColliders1.Length)-1].gameObject.tag;
                if (IsFoodWebValid(startTag, endTag))
                {
                    if(ScoreUpdate1!=null)
                    {
                        previousPosition = Vector3.zero;
                        line.positionCount=1;
                        ScoreUpdate1();
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
     private bool IsFoodWebValid(string startTag, string endTag)
    {
        if (string.IsNullOrEmpty(startTag) || string.IsNullOrEmpty(endTag))
        {
            return false;
        }

        int startIndex = System.Array.IndexOf(entities, startTag);
        int endIndex = System.Array.IndexOf(entities, endTag);

        if (startIndex == -1 || endIndex == -1)
        {
            return false;
        }

        if (System.Array.Exists(foodWeb[startIndex], target => target == endTag) && mappedConnections[startIndex, endIndex] == 0)
        {
            mappedConnections[startIndex, endIndex] = 1;
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

