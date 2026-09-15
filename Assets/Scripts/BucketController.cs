using UnityEngine;
public class BasketController : MonoBehaviour
{
    [Header("Movement")]
    public float smoothing = 0.25f;
    public float minX = -4f;
    public float maxX = 4f;
    
    [Header("SO")]
    public Color_bucket[]  buckets;

    [Header("Another")] 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioClip success;
    public ScoreManager score;

    private SpriteRenderer sr;
    private Camera cam;
    private bool isDragging;
    private float dragOffsetX;
    private float targetX;

    private Color_bucket current;
    private float colorDuration = 5f;

    private void Start()
    {
        cam = Camera.main;
        targetX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        SetRandomColor();
    }

    private void Update()
    {
        HandleInput();
        MoveBasket();
        CheckTime();
    }

    private void CheckTime()
    {
        colorDuration -= Time.deltaTime;

        if (colorDuration <= 0f)
        {
            colorDuration = 5f;
            SetRandomColor();
            audioSource.PlayOneShot(success);
        }
    }
    
    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = ScreenToWorld(Input.mousePosition);
            if (IsPointOnBasket(worldPos))
            {
                isDragging = true;
                dragOffsetX = transform.position.x - worldPos.x;
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 worldPos = ScreenToWorld(Input.mousePosition);
            targetX = worldPos.x + dragOffsetX;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldPos = ScreenToWorld(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isDragging = true;
                    dragOffsetX = transform.position.x - worldPos.x;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isDragging)
                        targetX = worldPos.x + dragOffsetX;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }

    private Vector3 ScreenToWorld(Vector3 screenPos)
    {
        screenPos.z = Mathf.Abs(cam.transform.position.z - transform.position.z);
        return cam.ScreenToWorldPoint(screenPos);
    }

    private bool IsPointOnBasket(Vector3 worldPos)
    {
        float halfWidth = sr.bounds.extents.x + 0.3f;
        return Mathf.Abs(worldPos.x - transform.position.x) <= halfWidth;
    }

    private void MoveBasket()
    {
        float clampedTarget = ClampToScreenBounds(targetX);

        Vector3 pos = transform.position;
        if (smoothing > 0f)
            pos.x = Mathf.Lerp(pos.x, clampedTarget, 1f - Mathf.Pow(smoothing, Time.deltaTime * 10f));
        else
            pos.x = clampedTarget;

        transform.position = pos;
    }

    private float ClampToScreenBounds(float x)
    {
        float min, max;
            float dist = Mathf.Abs(cam.transform.position.z - transform.position.z);
            Vector3 leftEdge = cam.ScreenToWorldPoint(new Vector3(0, 0, dist));
            Vector3 rightEdge = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, dist));
            float halfWidth = sr.bounds.extents.x;
            min = leftEdge.x + halfWidth;
            max = rightEdge.x - halfWidth;

        return Mathf.Clamp(x, min, max);
    }
 
    private void SetRandomColor()
    {
        Color_bucket newColor = buckets[Random.Range(0, buckets.Length)];
        while(current == newColor)
            newColor = buckets[Random.Range(0, buckets.Length)];
        
        current = newColor;
        sr.sprite =  current.sprite;
    }
 
    // ---------- Столкновение с падающей фигурой ----------
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ball shape = other.GetComponent<Ball>();
        if (shape == null) return;
 
        bool colorMatch = current.color == shape.current.color;
        Destroy(other.gameObject);
        
        if (colorMatch)
        {
            score.AddScore();
            audioSource.PlayOneShot(success);
        }        
        else
        {
            score.SubtractScore();
            audioSource.PlayOneShot(fail);
        } 
    }
}