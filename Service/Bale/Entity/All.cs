//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace Bale.Models;

//// Configure your HTTP serializer with BaleJson.Options.
//public static class BaleJson
//{
//    public static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
//    {
//        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
//        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
//        PropertyNameCaseInsensitive = true
//    };
//}

//public sealed class ApiResponse<T>
//{
//    public bool Ok { get; init; }
//    public T? Result { get; init; }
//    public string? Description { get; init; }
//    public int? ErrorCode { get; init; }
//    public ResponseParameters? Parameters { get; init; }
//}



//public sealed class WebhookInfo
//{
//    public string Url { get; init; } = "";
//}

//public sealed class User
//{
//    public long? Id { get; init; }
//    public bool IsBot { get; init; }
//    public string FirstName { get; init; } = "";
//    public string? LastName { get; init; }
//    public string? Username { get; init; }
//    public string? LanguageCode { get; init; }
//}



//public sealed class ChatFullInfo : Chat
//{
//    public ChatPhoto? Photo { get; init; }
//    public string? Bio { get; init; }
//    public string? Description { get; init; }
//    public string? InviteLink { get; init; }
//    public string? LinkedChatId { get; init; }
//}

//public sealed class Message
//{
//    public int MessageId { get; init; }
//    [JsonPropertyName("from")]
//    public User? From { get; init; }
//    public int Date { get; init; }
//    public Chat Chat { get; init; } = new();
//    public Chat? SenderChat { get; init; }
//    public User? ForwardFrom { get; init; }
//    public Chat? ForwardFromChat { get; init; }
//    public int? ForwardFromMessageId { get; init; }
//    public int? ForwardDate { get; init; }
//    public Message? ReplyToMessage { get; init; }
//    public int? EditDate { get; init; }
//    public string? MediaGroupId { get; init; }
//    public string? Text { get; init; }
//    public MessageEntity[]? Entities { get; init; }
//    public Animation? Animation { get; init; }
//    public Audio? Audio { get; init; }
//    public Document? Document { get; init; }
//    public PhotoSize[]? Photo { get; init; }
//    public Sticker? Sticker { get; init; }
//    public Video? Video { get; init; }
//    public Voice? Voice { get; init; }
//    public string? Caption { get; init; }
//    public MessageEntity[]? CaptionEntities { get; init; }
//    public Contact? Contact { get; init; }
//    public Location? Location { get; init; }
//    public User[]? NewChatMembers { get; init; }
//    public User? LeftChatMember { get; init; }
//    public Invoice? Invoice { get; init; }
//    public SuccessfulPayment? SuccessfulPayment { get; init; }
//    public WebAppData? WebAppData { get; init; }
//    public InlineKeyboardMarkup? ReplyMarkup { get; init; }
//}

//public sealed class MessageId
//{
//    public int MessageId { get; init; }
//}

//public sealed class MessageEntity
//{
//    public string Type { get; init; } = "";
//    public int Offset { get; init; }
//    public int Length { get; init; }
//}

//public sealed class PhotoSize
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public int Width { get; init; }
//    public int Height { get; init; }
//    public int? FileSize { get; init; }
//}

//public sealed class Animation
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public int Width { get; init; }
//    public int Height { get; init; }
//    public int Duration { get; init; }
//    public PhotoSize? Thumbnail { get; init; }
//    public string? FileName { get; init; }
//    public string? MimeType { get; init; }
//    public long? FileSize { get; init; }
//}

//public sealed class Audio
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public int Duration { get; init; }
//    public string? Title { get; init; }
//    public string? FileName { get; init; }
//    public string? MimeType { get; init; }
//    public long? FileSize { get; init; }
//}

//public sealed class Document
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public PhotoSize? Thumbnail { get; init; }
//    public string? FileName { get; init; }
//    public string? MimeType { get; init; }
//    public long? FileSize { get; init; }
//}

//public sealed class Video
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public int Width { get; init; }
//    public int Height { get; init; }
//    public int Duration { get; init; }
//    public string? FileName { get; init; }
//    public string? MimeType { get; init; }
//    public long? FileSize { get; init; }
//}

//public sealed class Voice
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//}


//public sealed class Location
//{
//    public double Longitude { get; init; }
//    public double Latitude { get; init; }
//}

//public sealed class Invoice
//{
//    public string Title { get; init; } = "";
//    public string Description { get; init; } = "";
//    public int TotalAmount { get; init; }
//}

//public sealed class File
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public long? FileSize { get; init; }
//    public string? FilePath { get; init; }
//}

//public sealed class ReplyKeyboardMarkup
//{
//    public KeyboardButton[][] Keyboard { get; init; } = [];
//}

//public sealed class KeyboardButton
//{
//    public string Text { get; init; } = "";
//    public bool? RequestContact { get; init; }
//    public bool? RequestLocation { get; init; }
//    public WebAppInfo? WebApp { get; init; }
//}

//public sealed class InlineKeyboardMarkup
//{
//    public InlineKeyboardButton[][] InlineKeyboard { get; init; } = [];
//}

//public sealed class InlineKeyboardButton
//{
//    public string Text { get; init; } = "";
//    public string? Url { get; init; }
//    public string? CallbackData { get; init; }
//    public WebAppInfo? WebApp { get; init; }
//    public CopyTextButton? CopyText { get; init; }
//}

//public sealed class ReplyKeyboardRemove
//{
//    public bool RemoveKeyboard { get; init; } = true;
//}


//public sealed class WebAppData
//{
//    public string Data { get; init; } = "";
//}

//public sealed class WebAppInfo
//{
//    public string Url { get; init; } = "";
//}

//public sealed class CopyTextButton
//{
//    public string Text { get; init; } = "";
//}

//// ChatMember is a discriminated JSON family. Deserialize into the matching
//// concrete subtype based on the status field when subtype-specific data is needed.
//public class ChatMember
//{
//    public string Status { get; init; } = "";
//    public User User { get; init; } = new();
//}

//public sealed class ChatMemberOwner : ChatMember;

//public sealed class ChatMemberAdministrator : ChatMember
//{
//    public bool CanDeleteMessages { get; init; }
//    public bool CanManageVideoChats { get; init; }
//    public bool CanRestrictMembers { get; init; }
//    public bool CanPromoteMembers { get; init; }
//    public bool CanChangeInfo { get; init; }
//    public bool CanInviteUsers { get; init; }
//    public bool CanPostStories { get; init; }
//    public bool? CanPostMessages { get; init; }
//    public bool? CanEditMessages { get; init; }
//    public bool? CanPinMessages { get; init; }
//}

//public sealed class ChatMemberMember : ChatMember;

//public sealed class ChatMemberRestricted : ChatMember
//{
//    public bool IsMember { get; init; }
//    public bool CanSendMessages { get; init; }
//    public bool CanSendAudios { get; init; }
//    public bool CanSendDocuments { get; init; }
//    public bool CanSendPhotos { get; init; }
//    public bool CanSendVideos { get; init; }
//    public bool CanChangeInfo { get; init; }
//    public bool CanInviteUsers { get; init; }
//    public bool CanPinMessages { get; init; }
//}

//public sealed class ChatPhoto
//{
//    public string SmallFileId { get; init; } = "";
//    public string SmallFileUniqueId { get; init; } = "";
//    public string BigFileId { get; init; } = "";
//    public string BigFileUniqueId { get; init; } = "";
//}

//public sealed class ResponseParameters
//{
//    public int? RetryAfter { get; init; }
//}

//// InputFile is uploaded as multipart/form-data and cannot be modeled as JSON alone.
//public sealed class InputFile
//{
//    public string FileName { get; init; } = "";
//    public Stream Content { get; init; } = Stream.Null;
//    public string? ContentType { get; init; }
//}

//public abstract class InputMedia
//{
//    public abstract string Type { get; }
//    public string Media { get; init; } = "";
//    public string? Caption { get; init; }
//}

//public sealed class InputMediaPhoto : InputMedia
//{
//    public override string Type => "photo";
//}

//public sealed class InputMediaVideo : InputMedia
//{
//    public override string Type => "video";
//    public string? Thumbnail { get; init; }
//    public int? Width { get; init; }
//    public int? Height { get; init; }
//    public int? Duration { get; init; }
//}

//public sealed class InputMediaAnimation : InputMedia
//{
//    public override string Type => "animation";
//    public string? Thumbnail { get; init; }
//    public int? Width { get; init; }
//    public int? Height { get; init; }
//    public int? Duration { get; init; }
//}

//public sealed class InputMediaAudio : InputMedia
//{
//    public override string Type => "audio";
//    public string? Thumbnail { get; init; }
//    public int? Duration { get; init; }
//    public string? Title { get; init; }
//}

//public sealed class InputMediaDocument : InputMedia
//{
//    public override string Type => "document";
//    public string? Thumbnail { get; init; }
//}

//// Additional documented entities referenced by Message and Update.
//public sealed class Sticker
//{
//    public string FileId { get; init; } = "";
//    public string FileUniqueId { get; init; } = "";
//    public string Type { get; init; } = "";
//    public int Width { get; init; }
//    public int Height { get; init; }
//    public long? FileSize { get; init; }
//}

//public sealed class StickerSet
//{
//    public string Name { get; init; } = "";
//    public string Title { get; init; } = "";
//    public Sticker[] Stickers { get; init; } = [];
//    public PhotoSize? Thumbnail { get; init; }
//}

//public sealed class LabeledPrice
//{
//    public string Label { get; init; } = "";
//    public int Amount { get; init; }
//}



//public sealed class SuccessfulPayment
//{
//    public string Currency { get; init; } = "";
//    public int TotalAmount { get; init; }
//    public string InvoicePayload { get; init; } = "";
//    public string TelegramPaymentChargeId { get; init; } = "";
//    public string ProviderPaymentChargeId { get; init; } = "";
//}

//public sealed class Transaction
//{
//    public string Id { get; init; } = "";
//    public string Status { get; init; } = "";
//    public int UserId { get; init; }
//    public int Amount { get; init; }
//    public int CreatedAt { get; init; }
//}
