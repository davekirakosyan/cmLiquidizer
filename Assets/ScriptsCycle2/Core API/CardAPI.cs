using UnityEngine;
using PathCreation;
using System.Collections;
using System.Collections.Generic;

public class CardAPI<genCardType, genElixir> : MonoBehaviour
{
    public Card<genCardType, genElixir> CreateCard(int level, genCardType cardType, List<genElixir> input, List<genElixir> output)
    {
        return new Card<genCardType, genElixir>(level, cardType, input, output);
    }

    public void LoadCard(int worldNumber)
    {
        // Need to instaniate cards for current world.
    }

    public void SelectedCard(ref Card<genCardType, genElixir> card)
    {
        // Called from CardOnclick event afterCard animation
        // Need to load own level
    }

    public void AnimateCard(genCardType cardType)
    {
        /* Called from CardOnclick event 1st
         * Depend on cardType the function can animate:
         *  Selected Card
         *  WinCard
         *  LoseCard
         *  Unselected cards
         */
    }

    public void CardStatusUpdate(ref Card<genCardType, genElixir> card)
    {
        // Add completion filter to the card.
        card.CompletedStatus = true;
    }
}
