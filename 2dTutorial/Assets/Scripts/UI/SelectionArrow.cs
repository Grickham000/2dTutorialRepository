using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private AudioClip changeSound;//moving arrow up and down
    [SerializeField] private AudioClip selectionSound; //hiting enter
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S ))
            ChangePosition(1);

        //
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change !=0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        //Debug.Log(currentPosition);
        //Debug.Log(options.Length);
        // Assign the Y position of the current option to the arrow (moving up and down)
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);

    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(selectionSound);

        //Access the button component on each option and call it's function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();

    }


}
