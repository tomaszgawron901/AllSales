using AllSales.Shared.Enums;
using Notion.Client;

namespace NotionOutput.Mapping;

internal static class NotionMapping
{
    /// <summary>
    /// Maps a <see cref="GenderType"/> to a <see cref="SelectOption"/>.
    /// </summary>
    /// <param name="genderType"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static SelectPropertyValue MapGenderToProperty(GenderType genderType)
    {
        string gender;
        if(genderType is GenderType.Male)
        {
            gender = "Male";
        }
        else if(genderType is GenderType.Female)
        {
            gender = "Female";
        }
        else
        {
            throw new FormatException();
        }

        return new SelectPropertyValue
        {
            Select = new SelectOption { Name = gender },
        };
    }
}
