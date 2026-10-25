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
    SetUsername,
    CreateUser,
    SetMobile,
    Sell,
    Buy
}

public enum Step
{
    WaitingForEnterGeram,
    WaitingForConfirmOrder,
    Completed,
}