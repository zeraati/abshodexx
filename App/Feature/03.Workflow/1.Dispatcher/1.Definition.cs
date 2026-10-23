namespace App.Feature;

public enum Button
{
    Start,
    Buy,
    Sell,
    Price
}

public enum Flow
{
    Sell,
    Buy
}

public enum Step
{
    WaitingForEnterGeram,
    WaitingForConfirmOrder,
    Completed,
}