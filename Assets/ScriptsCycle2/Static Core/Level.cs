using System;
using System.Collections.Generic;

public abstract class genLevel<genElixir>
{
    protected int levelIndex;

    protected float elixirSpeed;
    protected float elixirLength;

    protected float expectedTime;
    protected float completionTime;

    protected List<genElixir> inputColors;
    protected List<genElixir> outputColors;
}

public class Level<genElixir> : genLevel<genElixir>
{
    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    public float ElixirSpeed { get => elixirSpeed; set => elixirSpeed = value; }
    public float ElixirLength { get => elixirLength; set => elixirLength = value; }
    public float ExpectedTime { get => expectedTime; set => expectedTime = value; }
    public float CompletionTime { get => completionTime; set => completionTime = value; }
    public List<genElixir> InputColors { get => inputColors; set => inputColors = value; }
    public List<genElixir> OutputColors { get => outputColors; set => outputColors = value; }

    public Level(int levelIndex, float elixirSpeed, float elixirLength, float expectedTime, float completionTime, List<genElixir> inputColors, List<genElixir> outputColors)
    {
        LevelIndex = levelIndex;
        ElixirSpeed = elixirSpeed;
        ElixirLength = elixirLength;
        ExpectedTime = expectedTime;
        CompletionTime = completionTime;
        InputColors = inputColors ?? throw new ArgumentNullException(nameof(inputColors));
        OutputColors = outputColors ?? throw new ArgumentNullException(nameof(outputColors));
    }
}
