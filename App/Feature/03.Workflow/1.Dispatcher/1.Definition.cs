namespace App.Feature;

public enum Button
{
    SetUsername,
    Start,
    CreateUser,
    RequestContact,
    SetMobile,
    
    Price,
    Buy,
    Sell,
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