using UnityEngine;
using System;

public class Score
{
    private readonly int _step;

    public int Value { get; private set; }
    public event Action<int> OnChanged;

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