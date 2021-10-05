using System;

namespace AirSnitch.Domain.Models
{
    public class ApiUserProfile
    {
        public UserName Name {get;set;}
        public LastName LastName {get;set;}
        public ProfilePicture ProfilePic { get; set; }
        public Email Email {get;set;}

        public DateTime CreatedOn { get; set; }
        public GenderValue Gender { get; set; }
    }


    public struct Gender
    {
        public static GenderValue FromString(string gender)
        {
            return GenderValue.Male;
        }
    }

    public enum GenderValue
    {
        Male,
        Female,
        Other
    }

    public class ProfilePicture
    {
        public ProfilePicture(string profilePicUrl)
        {
            Value = profilePicUrl;
        }

        public string Value { get; }
    }
}