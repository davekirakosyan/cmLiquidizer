using System;
using System.Collections.Generic;

public abstract class genLevel<genInventoryManger, genCardTemplatesManager>
{
    protected float elixirLength;
    protected float elixirSpeed;

    protected List<genInventoryManger> inputColors;
    protected List<genInventoryManger> outputColors;

    protected genCardTemplatesManager instructionsTemplate;

}

public class Level<genInventoryManger, genCardTemplatesManager> : genLevel<genInventoryManger, genCardTemplatesManager>
{

    public float ElixirLength { get => elixirLength; set => elixirLength = value; }
    public float ElixirSpeed { get => elixirSpeed; set => elixirSpeed = value; }
    public List<genInventoryManger> InputColors { get => inputColors; set => inputColors = value; }
    public List<genInventoryManger> OutputColors { get => outputColors; set => outputColors = value; }
    public genCardTemplatesManager InstructionsTemplate { get => instructionsTemplate; set => instructionsTemplate = value; }

    public Level(float elixirLength, float elixirSpeed, List<genInventoryManger> inputColors, List<genInventoryManger> outputColors, genCardTemplatesManager instructionsTemplate)
    {
        ElixirLength = elixirLength;
        ElixirSpeed = elixirSpeed;
        InputColors = inputColors ?? throw new ArgumentNullException(nameof(inputColors));
        OutputColors = outputColors ?? throw new ArgumentNullException(nameof(outputColors));
        InstructionsTemplate = instructionsTemplate;
    }
}
