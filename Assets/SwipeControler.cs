using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage = 1;

    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime = 0.5f;
    [SerializeField] LeanTweenType tweenType = LeanTweenType.easeInOutCubic;

    Vector2 startTouchPos;
    Vector2 endTouchPos;
    [SerializeField] float swipeThreshold = 100f; // min distance to detect swipe

    void Start()
    {
        targetPos = levelPagesRect.localPosition;
    }

    public void Next()
    {
        // Move down
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep; // Make sure pageStep.y is negative if down means next
            MovePage();
        }
    }

    public void Previous()
    {
        // Move up
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Mouse drag for testing in editor
        if (Input.GetMouseButtonDown(0))
            startTouchPos = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPos = Input.mousePosition;
            DetectSwipe();
        }
#else
        // Touch input for mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                startTouchPos = touch.position;
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPos = touch.position;
                DetectSwipe();
            }
        }
#endif
    }

    void DetectSwipe()
    {
        float swipeDistY = endTouchPos.y - startTouchPos.y;

        if (Mathf.Abs(swipeDistY) > swipeThreshold)
        {
            if (swipeDistY < 0)
                Next();     // swipe up -> next page (downward scroll)
            else
                Previous(); // swipe down -> previous page (upward scroll)
        }
    }
}
