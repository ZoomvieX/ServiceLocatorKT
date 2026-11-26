using UnityEngine;
using System;
using Zenject;

public class Score
{
    private readonly int _step;

    public int Value { get; private set; }
    public event Action<int> OnChanged;

    [Inject]
    public Score(int step = 1, int initialValue = 0)
    {
        _step = step;
        Value = initialValue;
    }

    public void Add()
    {
        Value += _step;
        OnChanged?.Invoke(Value);
    }
}