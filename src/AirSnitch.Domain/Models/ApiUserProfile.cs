using System;

namespace AirSnitch.Domain.Models
{
    public class ApiUserProfile
    {
        public UserName Name {get;set;}

        public string GetDisplayName()
        {
            return Name.Value;
        }
        
        public LastName LastName {get;set;}
        
        public string GetLastName()
        {
            return LastName.Value;
        }

        public ProfilePicture ProfilePic { get; set; }

        public string GetProfilePicUrl()
        {
            return ProfilePic.Value;
        }

        public Email Email {get;set;}

        public string GetEmailValue()
        {
            return Email.Value;
        }

        public DateTime CreatedOn { get; set; }
        
        public string GetGenderValue()
        {
            return Gender.ToString();
        }
        
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