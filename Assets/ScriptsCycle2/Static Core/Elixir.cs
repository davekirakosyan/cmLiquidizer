using System;
using System.Collections;
using System.Collections.Generic;

public abstract class genElixir<genColor>
{
    protected string name;
    protected float speed;
    protected float lenght;
    protected genColor color;
}

public class Elixir<genColor> : genElixir<genColor>
{
    public string Name { get => name; set => name = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Lenght { get => lenght; set => lenght = value; }
    public genColor Color { get => color; set => color = value; }

    public Elixir(string name, float speed, float lenght, genColor color)
    {
        Speed = speed;
        Color = color;
        Lenght = lenght;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
