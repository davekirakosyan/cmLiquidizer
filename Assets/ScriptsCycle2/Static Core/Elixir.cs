using System;
using System.Collections;
using System.Collections.Generic;

public abstract class genElixir<genColor, genElixirAbility>
{
    protected int index;
    protected string name;
    protected genColor color;
    protected genElixirAbility ability;
}

public class Elixir<genColor, genElixirAbility> : genElixir<genColor, genElixirAbility>
{
    public int Index { get => index; set => index = value; }
    public string Name { get => name; set => name = value; }
    public genColor Color { get => color; set => color = value; }
    public genElixirAbility Ability { get => ability; set => ability = value; }

    public Elixir(int index, string name, genColor color, genElixirAbility ability)
    {
        Index = index;
        Color = color;
        Ability = ability;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
