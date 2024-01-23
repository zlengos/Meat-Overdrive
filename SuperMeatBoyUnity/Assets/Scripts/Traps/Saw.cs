using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saw : Traps
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sawSomeBlood, _sawMoreBlood, _sawMaxBlood;
    private int _bloodLevel = 0;
    [SerializeField] private float _rotationSpeed = 200f;

    private void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();

    private void Update() => transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);

    public override void Killed()
    {
        _bloodLevel++;
        switch (_bloodLevel)
        {
            case 1:
                _spriteRenderer.sprite = _sawSomeBlood;
                break;
            case 2:
                _spriteRenderer.sprite = _sawMoreBlood;
                break;
            case 3:
                _spriteRenderer.sprite = _sawMaxBlood;
                break;
        }
    }
}

    