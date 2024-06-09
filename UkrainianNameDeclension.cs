using System;

public class UkrainianNameDeclension
{
    public static string DeclineToGenitive(string gender, string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.");

        var parts = fullName.Split(' ');
        if (parts.Length != 3)
            throw new ArgumentException("Full name must consist of three parts: first name, patronymic, and last name.");

        var firstName = parts[0];
        var patronymic = parts[1];
        var lastName = parts[2];

        var declinedFirstName = DeclineFirstNameToGenitive(gender, firstName);
        var declinedPatronymic = DeclinePatronymicToGenitive(gender, patronymic);
        var declinedLastName = DeclineLastNameToGenitive(gender, lastName);

        return $"{declinedFirstName} {declinedPatronymic} {declinedLastName}";
    }

    public static string DeclineToDative(string gender, string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.");

        var parts = fullName.Split(' ');
        if (parts.Length != 3)
            throw new ArgumentException("Full name must consist of three parts: first name, patronymic, and last name.");

        var firstName = parts[0];
        var patronymic = parts[1];
        var lastName = parts[2];

        var declinedFirstName = DeclineFirstNameToDative(gender, firstName);
        var declinedPatronymic = DeclinePatronymicToDative(gender, patronymic);
        var declinedLastName = DeclineLastNameToDative(gender, lastName);

        return $"{declinedFirstName} {declinedPatronymic} {declinedLastName}";
    }

    public static string DeclineRank(string rank, string caseType)
    {
        if (string.IsNullOrWhiteSpace(rank))
            throw new ArgumentException("Rank cannot be empty.");

        switch (caseType)
        {
            case "Genitive":
                return DeclineRankToGenitive(rank);
            case "Dative":
                return DeclineRankToDative(rank);
            default:
                throw new ArgumentException("Invalid case type.");
        }
    }

    private static string DeclineFirstNameToGenitive(string gender, string firstName)
    {
        if (gender == "Ч")
        {
            if (firstName.EndsWith("й"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "я";
            }
            else if (firstName.EndsWith("а"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "и";
            }
        }
        else if (gender == "Ж")
        {
            if (firstName.EndsWith("а"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "и";
            }
            else if (firstName.EndsWith("я"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "ї";
            }
        }
        return firstName;
    }

    private static string DeclineFirstNameToDative(string gender, string firstName)
    {
        if (gender == "Ч")
        {
            if (firstName.EndsWith("й"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "ю";
            }
            else if (firstName.EndsWith("а"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "і";
            }
        }
        else if (gender == "Ж")
        {
            if (firstName.EndsWith("а"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "і";
            }
            else if (firstName.EndsWith("я"))
            {
                return firstName.Substring(0, firstName.Length - 1) + "ї";
            }
        }
        return firstName;
    }

    private static string DeclinePatronymicToGenitive(string gender, string patronymic)
    {
        if (gender == "Ч")
        {
            if (patronymic.EndsWith("ич"))
            {
                return patronymic + "а";
            }
        }
        else if (gender == "Ж")
        {
            if (patronymic.EndsWith("на"))
            {
                return patronymic.Substring(0, patronymic.Length - 1) + "и";
            }
        }
        return patronymic;
    }

    private static string DeclinePatronymicToDative(string gender, string patronymic)
    {
        if (gender == "Ч")
        {
            if (patronymic.EndsWith("ич"))
            {
                return patronymic + "у";
            }
        }
        else if (gender == "Ж")
        {
            if (patronymic.EndsWith("на"))
            {
                return patronymic.Substring(0, patronymic.Length - 1) + "ні";
            }
        }
        return patronymic;
    }

    private static string DeclineLastNameToGenitive(string gender, string lastName)
    {
        if (gender == "Ч")
        {
            if (lastName.EndsWith("ий") || lastName.EndsWith("ій"))
            {
                return lastName.Substring(0, lastName.Length - 2) + "ого";
            }
            else if (lastName.EndsWith("ь"))
            {
                return lastName + "а";
            }
        }
        else if (gender == "Ж")
        {
            if (lastName.EndsWith("а"))
            {
                return lastName.Substring(0, lastName.Length - 1) + "ої";
            }
        }
        return lastName;
    }

    private static string DeclineLastNameToDative(string gender, string lastName)
    {
        if (gender == "Ч")
        {
            if (lastName.EndsWith("ий") || lastName.EndsWith("ій"))
            {
                return lastName.Substring(0, lastName.Length - 2) + "ому";
            }
            else if (lastName.EndsWith("ь"))
            {
                return lastName + "ю";
            }
        }
        else if (gender == "Ж")
        {
            if (lastName.EndsWith("а"))
            {
                return lastName.Substring(0, lastName.Length - 1) + "ій";
            }
        }
        return lastName;
    }

    private static string DeclineRankToGenitive(string rank)
    { 
        return rank + "а";
    }

    private static string DeclineRankToDative(string rank)
    {
        return rank + "у";
    }
}
