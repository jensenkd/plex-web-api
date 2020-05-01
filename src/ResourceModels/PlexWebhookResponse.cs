using System;
using System.Text.Json.Serialization;
using Plex.Api.Models;
using Plex.Api.Models.Server;
using Plex.Api.Models.Status;

namespace Plex.Web.Api.ResourceModels
{
       public class PlexWebhookResponse
    {
        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("user")]
        public bool User { get; set; }

        [JsonPropertyName("owner")]
        public bool Owner { get; set; }

        [JsonPropertyName("Account")]
        public Account Account { get; set; }

        [JsonPropertyName("Server")]
        public Server Server { get; set; }

        [JsonPropertyName("Player")]
        public Player Player { get; set; }

        [JsonPropertyName("Metadata")]
        public Metadata Metadata { get; set; }
    }

    public class Account
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    
        [JsonPropertyName("thumb")]
        public Uri Thumb { get; set; }
    
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
    //
    // public class Metadata
    // {
    //     [JsonPropertyName("librarySectionType")]
    //     public string LibrarySectionType { get; set; }
    //
    //     [JsonPropertyName("ratingKey")]
    //     public string RatingKey { get; set; }
    //
    //     [JsonPropertyName("key")]
    //     public string Key { get; set; }
    //
    //     [JsonPropertyName("parentRatingKey")]
    //     public string ParentRatingKey { get; set; }
    //
    //     [JsonPropertyName("grandparentRatingKey")]
    //      public string GrandparentRatingKey { get; set; }
    //
    //     [JsonPropertyName("guid")]
    //     public string Guid { get; set; }
    //
    //     [JsonPropertyName("librarySectionID")]
    //     public long LibrarySectionId { get; set; }
    //
    //     [JsonPropertyName("type")]
    //     public string Type { get; set; }
    //
    //     [JsonPropertyName("title")]
    //     public string Title { get; set; }
    //
    //     [JsonPropertyName("grandparentKey")]
    //     public string GrandparentKey { get; set; }
    //
    //     [JsonPropertyName("parentKey")]
    //     public string ParentKey { get; set; }
    //
    //     [JsonPropertyName("grandparentTitle")]
    //     public string GrandparentTitle { get; set; }
    //
    //     [JsonPropertyName("parentTitle")]
    //     public string ParentTitle { get; set; }
    //
    //     [JsonPropertyName("summary")]
    //     public string Summary { get; set; }
    //
    //     [JsonPropertyName("index")]
    //     public long Index { get; set; }
    //
    //     [JsonPropertyName("parentIndex")]
    //     public long ParentIndex { get; set; }
    //
    //     [JsonPropertyName("ratingCount")]
    //     public long RatingCount { get; set; }
    //
    //     [JsonPropertyName("thumb")]
    //     public string Thumb { get; set; }
    //
    //     [JsonPropertyName("art")]
    //     public string Art { get; set; }
    //
    //     [JsonPropertyName("parentThumb")]
    //     public string ParentThumb { get; set; }
    //
    //     [JsonPropertyName("grandparentThumb")]
    //     public string GrandparentThumb { get; set; }
    //
    //     [JsonPropertyName("grandparentArt")]
    //     public string GrandparentArt { get; set; }
    //
    //     [JsonPropertyName("addedAt")]
    //     public long AddedAt { get; set; }
    //
    //     [JsonPropertyName("updatedAt")]
    //     public long UpdatedAt { get; set; }
    // }
    //
    // public partial class Player
    // {
    //     [JsonPropertyName("local")]
    //     public bool Local { get; set; }
    //
    //     [JsonPropertyName("publicAddress")]
    //     public string PublicAddress { get; set; }
    //
    //     [JsonPropertyName("title")]
    //     public string Title { get; set; }
    //
    //     [JsonPropertyName("uuid")]
    //     public string Uuid { get; set; }
    // }
    //
    // public class Server
    // {
    //     [JsonPropertyName("title")]
    //     public string Title { get; set; }
    //
    //     [JsonPropertyName("uuid")]
    //     public string Uuid { get; set; }
    // }
}