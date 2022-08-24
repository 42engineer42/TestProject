using System.ComponentModel.DataAnnotations;

namespace Nullam.Facade;
public class PersonalCodeValidation : ValidationAttribute {
    public override bool IsValid(object value) {
        string? code = value as string;
        if (code == null || !code.All(char.IsDigit) || !code.Length.Equals(11)) return false;

        string codeGender = code[..1];
        string codeBirthYear = code.Substring(1, 2);
        string codeBirthMonth = code.Substring(3, 2);
        string codeBirthDay = code.Substring(5, 2);
        string codeBirthOrder = code.Substring(7, 3);
        string codeController = code.Substring(10, 1);

        int year = int.Parse(codeBirthYear);
        year += year > DateTime.Today.Year - 2000 ? 1900 : 2000; //assume that person participating in the event is not born before 1900
        int month = int.Parse(codeBirthMonth);
        int day = int.Parse(codeBirthDay);

        if (!IsGenderCorrect(codeGender)) return false;
        if (!IsBirthdayCorrect(year, month, day)) return false;
        if (!AreGenderAndYearMatching(year, codeGender)) return false;
        if (!IsControlNumberCorrect(code, codeController)) return false;
        return true;
    }
    private static bool IsGenderCorrect(string num) => int.Parse(num) >= 3 || int.Parse(num) <= 6;
    private static bool IsBirthdayCorrect(int year, int month, int day)
        => DateTime.TryParse(day + "/" + month + "/" + year, out DateTime _);
    private static bool AreGenderAndYearMatching(int year, string gender) {
        switch (gender) {
            case "3" or "4" when year.ToString()[..2] == "19":
            case "5" or "6" when year.ToString()[..2] == "20":
                return true;
            default:
                return false;
        }
    }
    private static bool IsControlNumberCorrect(string code, string controlNum) {
        int[] firstRoundNums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
        int[] secondRoundNums = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
        char[] numbers = code[..^1].ToCharArray();

        bool IsControlNumberMatching(int[] nums) => PersonalCodeValidation.IsControlNumberMatching(numbers, controlNum, nums);

        if (IsControlNumberMatching(firstRoundNums)) return true;
        return IsControlNumberMatching(secondRoundNums);
    }
    private static bool IsControlNumberMatching(char[] numbers, string controlNum, int[] round) {
        int calculatedAmount = 0;
        for (int i = 0; i < numbers.Length; i++) calculatedAmount += int.Parse(numbers[i].ToString()) * round[i];
        return calculatedAmount % 11 == int.Parse(controlNum);
    }
}