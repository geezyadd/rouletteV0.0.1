using System;

public class CardChooseGameModel
{
    private bool _isCardSpin;
    private bool _isCardGame;
    public event Action OnIsCardSpinChanged;
    public event Action OnIsCardGameChanged;
    public bool IsCardSpin
    {
        get =>
            _isCardSpin;
        set
        {
            _isCardSpin = value;
            OnIsCardSpinChanged?.Invoke();
        }
    }

    public bool IsCardGame
    {
        get =>
            _isCardGame;
        set
        {
            _isCardGame = value;
            OnIsCardGameChanged?.Invoke();
        }
    }
    public CardType CardType { get; set; }
}
