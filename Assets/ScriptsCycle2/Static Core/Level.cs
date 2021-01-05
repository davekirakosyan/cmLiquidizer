using System;
using System.Collections.Generic;

public abstract class genLevel<genInventoryManger>
{
    protected int levelIndex;

    protected float elixirSpeed;
    protected float elixirLength;

    protected float expectedTime;
    protected float completionTime;

    protected List<genInventoryManger> inputColors;
    protected List<genInventoryManger> outputColors;
}

public class Level<genInventoryManger> : genLevel<genInventoryManger>
{

    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    public float ElixirSpeed { get => elixirSpeed; set => elixirSpeed = value; }
    public float ElixirLength { get => elixirLength; set => elixirLength = value; }
    public float ExpectedTime { get => expectedTime; set => expectedTime = value; }
    public float CompletionTime { get => completionTime; set => completionTime = value; }
    public List<genInventoryManger> InputColors { get => inputColors; set => inputColors = value; }
    public List<genInventoryManger> OutputColors { get => outputColors; set => outputColors = value; }
}
