using System;
using System.Collections;
using System.Collections.Generic;

public abstract class genCard<genCardType, genElixir>
{
    protected int level;
    protected bool completedStatus;
    protected genCardType cardType; 
    protected List<genElixir> input;
    protected List<genElixir> output;
}

public class Card<genCardType, genElixir> : genCard<genCardType, genElixir>
{
    public int Level { get => level; set => level = value; }
    public genCardType CardType { get => cardType; set => cardType = value; }
    public List<genElixir> InputColors { get => input; set => input = value; }
    public List<genElixir> OutputColors { get => output; set => output = value; }
    public bool CompletedStatus { get => completedStatus; set => completedStatus = value; }

    public Card(int level, genCardType cardType, List<genElixir> inputColors, List<genElixir> outputColors)
    {
        Level = level;
        CardType = cardType;
        CompletedStatus = false;
        InputColors = inputColors ?? throw new ArgumentNullException(nameof(inputColors));
        OutputColors = outputColors ?? throw new ArgumentNullException(nameof(outputColors));
    }
}
