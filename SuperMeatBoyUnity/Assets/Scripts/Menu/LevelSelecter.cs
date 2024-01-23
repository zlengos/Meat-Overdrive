using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecter : ButtonSelecter
{
    private int _currLines = 0;
    private int _unlockedLevels;

    private void Start()
    {
        //PlayerPrefs.SetInt("levels", 1);
        //PlayerPrefs.DeleteAll();
        _unlockedLevels = PlayerPrefs.GetInt("levels", 1);

        if(_unlockedLevels > _buttons.Length -1)
            _unlockedLevels = _buttons.Length;

        for (int i = 0; i <= _buttons.Length - 1; i++)
            _buttons[i].interactable = false;

        for (int i = 0; i <= _unlockedLevels - 1; i++)
        {
            Debug.Log(i);
            _buttons[i].interactable = true;

        }

        SelectButton(_currentButtonIndex);
        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
            gridLayout.enabled = false;

        for (int i = 0; i < _buttons.Length - 1; i++)
        {
            GameObject lineObject = new GameObject("Line" + _currLines);
            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            line.positionCount = 2; 
            line.material = new Material(Shader.Find("Sprites/Default"));


            line.SetPosition(0, _buttons[i].transform.position);
            line.startColor = Color.magenta;
            line.SetPosition(1, _buttons[i + 1].transform.position);
            line.sortingLayerID = 0;
            _currLines++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            SelectPreviousButton();

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            SelectNextButton();

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(int index) => SceneManager.LoadScene(index);


}
