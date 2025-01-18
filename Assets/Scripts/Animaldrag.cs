
/*using UnityEngine;

public class AnimalDrag : MonoBehaviour
{
    private Vector3 initialPosition; // To store the initial position of the animal
    private bool isDragging = false;
    private float zPosition; // To store the constant Z-coordinate

    void Start()
    {
        // Store the initial Z-coordinate of the animal
        zPosition = transform.position.z;
    }

    void OnMouseDown()
    {
        isDragging = true;
        initialPosition = transform.position; // Store the initial position
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Maintain Z-coordinate
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update position, keeping the Z-coordinate constant
        transform.position = new Vector3(worldPosition.x, worldPosition.y, zPosition);
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check if the animal is dropped on an ecosystem
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        if (hitCollider != null && hitCollider.CompareTag("EcoSystem"))
        {
            Debug.Log($"Dropped on: {hitCollider.name}");

            // Snap to the ecosystem's position (keep Z-coordinate constant)
            transform.position = new Vector3(hitCollider.transform.position.x, hitCollider.transform.position.y, zPosition);
        }
        else
        {
            // Return to the initial position if dropped elsewhere (keep Z-coordinate constant)
            transform.position = initialPosition;
        }
    }
}
/*

using UnityEngine;

public class AnimalDrag : MonoBehaviour
{
    private Vector3 initialPosition; // To store the initial position
    private bool isDragging = false; // Dragging flag
    public static bool isCurrentAnimalActive = true; // Controls sequential activation
    public AnimalDrag[] allAnimals; // Array of all animal scripts
    private int currentIndex = 0; // Tracks the current active animal
    private bool s = false;

    void Start()
    {
        initialPosition = transform.position; // Store the initial position

        // Activate only the first animal at the start
        if (currentIndex == 0)
        {
           
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (!isCurrentAnimalActive || !gameObject.activeSelf) return; // Prevent interaction if not active
        isDragging = true;
        initialPosition = transform.position; // Store the initial position
    }

    void OnMouseDrag()
    {
        if (!isDragging || !isCurrentAnimalActive) return;

        // Get mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(initialPosition).z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update position while keeping the Z-coordinate constant
        transform.position = new Vector3(worldPosition.x, worldPosition.y, initialPosition.z);
    }

    void OnMouseUp()
    {
        if (!isDragging || !isCurrentAnimalActive) return;

        isDragging = false;

        // Check if the animal is dropped on a valid ecosystem
        Collider2D hitCollider = GetColliderUnderMouse2D();
        if (hitCollider != null && hitCollider.CompareTag(GetEcosystemTagForAnimal()))
        {
            Debug.Log($"{gameObject.name} placed correctly in {hitCollider.name}!");
            s=true;
            // Deactivate the current animal
            //gameObject.GetComponent<Collider>().enabled = false; // Disable the collider
            gameObject.SetActive(false); // Deactivate the GameObject
            isCurrentAnimalActive = false;

            // Activate the next animal
            ActivateNextAnimal();
        }
        else
        {
            // Return to the initial position if not dropped on the correct ecosystem
            Debug.Log($"{gameObject.name} not placed correctly. Returning to initial position.");
            Debug.Log($"hitted{hitCollider.name}");

            ResetPosition();
            isCurrentAnimalActive = true; // Allow re-interaction
        }
    }

    private string GetEcosystemTagForAnimal()
    {
        Debug.Log(tag);
        // Map animal tags to ecosystem tags
        switch (tag)
        {
            case "Tiger": return "Greenforest";
            case "penguin": return "Ice";
            case "Deer": return "sandland";
            case "Polarbear": return "Ice";
            case "gorilla": return "Greenforest";
            case "Macow":return "Greenforest";
            case "Rhino":return "sandland";
            case "beluga": return "Ice";
            case "tuna": return "lake";
            case "octopus": return "lake";
            case "oryx": return "desert";
            case "Seaturtle": return "lake";
            case "kangaroo": return "sandland";
            case "Hillanimal": return "Hills";
            case "Cincilla": return "Hills";
            case "camel": return "desert";
            case "goat": return "Hills";
            case "giraffe": return "sandlands";
            case "Ostrich": return "desert";




            default: return ""; // Default case if no matching tag
        }
    }

    private void ActivateNextAnimal()
    {
        // Ensure we don't go out of bounds
        while (currentIndex < allAnimals.Length)
        {
            var nextAnimal = allAnimals[currentIndex];
            currentIndex++;

            if (!nextAnimal.gameObject.activeSelf)
            {
                Debug.Log($"{nextAnimal.gameObject.name} activated.");
                nextAnimal.gameObject.SetActive(true);
                //nextAnimal.GetComponent<Collider>().enabled = true; // Re-enable interaction
                isCurrentAnimalActive = true;
                return;
            }
        }

        // If no more animals are left to activate
        Debug.Log("All animals have been placed correctly. No more animals to activate.");
        isCurrentAnimalActive = false;
    }

    private Collider2D GetColliderUnderMouse2D()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.OverlapPoint(mousePosition);
    }

    private void ResetPosition()
    {
        Debug.Log($"{gameObject.name} is resetting to its initial position at {initialPosition}.");
        transform.position = initialPosition; // Reset position to its original state
    }
   void Update(){
     GameObject deer = GameObject.Find("Deer_001");  // Make sure "Deer" matches the name of your deer GameObject
    if (deer != null)
    {
        Debug.Log("Deer Active Status: " + deer.activeSelf);
    }

    if(s){
          GameObject[] deerObjects = GameObject.FindGameObjectsWithTag("Deer");

// Check if any deer objects were found
 if (deerObjects.Length > 0)
 {
    // Loop through each GameObject and deactivate it
    foreach (GameObject dee in deerObjects)
    {
        dee.SetActive(false); // Deactivate the GameObject
        ActivateNextAnimal();

    }
}

    }


    }

}
*/
using UnityEngine;

public class AnimalDrag : MonoBehaviour
{
    private Vector3 initialPosition; // To store the initial position
    private bool isDragging = false; // Dragging flag
    public static bool isCurrentAnimalActive = true; // Controls sequential activation
    public AnimalDrag[] allAnimals; // Array of all animal scripts
    private int currentIndex = 0; // Tracks the current active animal
    private bool s = false;
 private int k=0;
    private Collider2D animalCollider; // Reference to the object's collider

    void Start()
    {
        initialPosition = transform.position; 
        animalCollider = GetComponent<Collider2D>(); 

        // Activate only the first animal at the start
        if (currentIndex == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (!isCurrentAnimalActive || !gameObject.activeSelf) return; // Prevent interaction if not active
        isDragging = true;
        initialPosition = transform.position; 

        // Temporarily disable the collider to avoid self-collision
        if (animalCollider != null)
        {
            animalCollider.enabled = false;
            Debug.Log($"{gameObject.name}: Collider temporarily disabled to prevent self-collision.");

        }
    }

    void OnMouseDrag()
    {
        if (!isDragging || !isCurrentAnimalActive) return;

        // Get mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(initialPosition).z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update position while keeping the Z-coordinate constant
        transform.position = new Vector3(worldPosition.x, worldPosition.y, initialPosition.z);
    }

    void OnMouseUp()
    {
        if (!isDragging || !isCurrentAnimalActive) return;

       
        // Re-enable the collider after drag is completed
       
        

        // Check if the animal is dropped on a valid ecosystem
        Collider2D hitCollider = GetColliderUnderMouse2D();
        if (hitCollider != null && hitCollider.CompareTag(GetEcosystemTagForAnimal()))
        {
           
            Debug.Log($"{gameObject.name} placed correctly in {hitCollider.name}!");
                           FindObjectOfType<GameController>()?.ObjectPlacedCorrectly();

            s = true;
            gameObject.SetActive(false);
            isCurrentAnimalActive = false;
             
            // Activate the next animal
            ActivateNextAnimal();
             
        }
        else
        {
            // Return to the initial position if not dropped on the correct ecosystem
            Debug.Log($"{gameObject.name} not placed correctly. Returning to initial position.");
            if (hitCollider != null)
                Debug.Log($"Hit object: {hitCollider.name}");

            ResetPosition();
            isCurrentAnimalActive = true; 
        }
       
            animalCollider.enabled = true;
        
         isDragging = false;
    }

    private string GetEcosystemTagForAnimal()
    {
        switch (tag)
        {
            case "Tiger": return "Greenforest";
            case "penguin": return "Ice";
            case "Deer": return "sandland";
            case "Polarbear": return "Ice";
            case "gorilla": return "Greenforest";
            case "Macow": return "Greenforest";
            case "Rhino": return "sandland";
            case "beluga": return "Ice";
            case "tuna": return "Lake";
            case "octopus": return "Lake";
            case "oryx": return "desert";
            case "Seaturtle": return "Lake";
            case "kangaroo": return "sandland";
            case "Hillanimal": return "Hills";
            case "Cincilla": return "Hills";
            case "camel": return "desert";
            case "goat": return "Hills";
            case "giraffe": return "sandland";
            case "Ostrich": return "desert";
            default: return ""; // Default case if no matching tag
        }
    }

    private void ActivateNextAnimal()
    {
        k++;
        while (currentIndex < allAnimals.Length)
        {
            var nextAnimal = allAnimals[currentIndex];
            currentIndex++;

            if (!nextAnimal.gameObject.activeSelf)
            {
                Debug.Log($"{nextAnimal.gameObject.name} activated.");
                nextAnimal.gameObject.SetActive(true);
                isCurrentAnimalActive = true;
                return;
            }
        }

        Debug.Log("All animals have been placed correctly. No more animals to activate.");
        isCurrentAnimalActive = false;
    }

    private Collider2D GetColliderUnderMouse2D()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.OverlapPoint(mousePosition);
    }

    private void ResetPosition()
    {
        Debug.Log($"{gameObject.name} is resetting to its initial position at {initialPosition}.");
        transform.position = initialPosition; // Reset position to its original state
    }

    void Update()
    {   
       
        GameObject deer = GameObject.Find("Deer_001"); // Make sure "Deer" matches the name of your deer GameObject
        if (deer != null)
        {
            Debug.Log("Deer Active Status: " + deer.activeSelf);
        }

        if (s)
        {
            GameObject[] deerObjects = GameObject.FindGameObjectsWithTag("Deer");
            
            if (deerObjects.Length > 0)
            {
                foreach (GameObject dee in deerObjects)
                {
                    dee.SetActive(false); 
                    ActivateNextAnimal();
                }
            }
        }
    }
}
