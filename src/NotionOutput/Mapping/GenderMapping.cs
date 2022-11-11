using AllSales.Shared.Enums;
using AllSales.Shared.Models;
using Notion.Client;

namespace NotionOutput.Mapping;

internal static class GenderMapping
{
    /// <summary>
    /// Maps a <see cref="GenderType"/> to a <see cref="SelectOption"/>.
    /// </summary>
    /// <param name="genderType"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static SelectPropertyValue MapGenderToProperty(GenderType genderType)
    {
        string? gender = null;
        if(genderType is GenderType.Male)
        {
            gender = "Male";
        }
        else if(genderType is GenderType.Female)
        {
            gender = "Female";
        }

        return new SelectPropertyValue
        {
            Select = new SelectOption { Name = gender },
        };
    }

    /// <exception cref="FormatException"></exception>
    public static GenderType? MapPropertyToGender(SelectPropertyValue propertyValue)
    {
        if(propertyValue?.Select is null)
        {
            return null;
        }

        if (propertyValue.Select.Name == "Male")
        {
            return GenderType.Male;
        }
        if (propertyValue.Select.Name == "Female")
        {
            return GenderType.Female;
        }

        return null;
    }
}
