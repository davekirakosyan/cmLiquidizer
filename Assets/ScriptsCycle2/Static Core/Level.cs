using System;
using System.Collections.Generic;

public abstract class genLevel<genElixir>
{
    protected int levelIndex;

    protected float elixirSpeed;
    protected float elixirLength;

    protected float expectedTime;
    protected float completionTime;

    protected List<genElixir> inputElixir;
    protected List<genElixir> outputElixir;
}

public class Level<genElixir> : genLevel<genElixir>
{
    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    public float ElixirSpeed { get => elixirSpeed; set => elixirSpeed = value; }
    public float ElixirLength { get => elixirLength; set => elixirLength = value; }
    public float ExpectedTime { get => expectedTime; set => expectedTime = value; }
    public float CompletionTime { get => completionTime; set => completionTime = value; }
    public List<genElixir> InputElixirs { get => inputElixir; set => inputElixir = value; }
    public List<genElixir> OutputElixirs { get => outputElixir; set => outputElixir = value; }

    public Level(int levelIndex, float elixirSpeed, float elixirLength, float expectedTime, float completionTime, List<genElixir> inputElixirs, List<genElixir> outputElixirs)
    {
        LevelIndex = levelIndex;
        ElixirSpeed = elixirSpeed;
        ElixirLength = elixirLength;
        ExpectedTime = expectedTime;
        CompletionTime = completionTime;
        InputElixirs = inputElixirs ?? throw new ArgumentNullException(nameof(inputElixirs));
        OutputElixirs = outputElixirs ?? throw new ArgumentNullException(nameof(outputElixirs));
    }

}
