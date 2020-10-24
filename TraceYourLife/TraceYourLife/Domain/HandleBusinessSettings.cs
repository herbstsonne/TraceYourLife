using System;
using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain
{
    public class HandleBusinessSettings
    {
        private readonly IPerson person;
        public HandleBusinessSettings(IPerson person)
        {
            this.person = person;
        }

        public string SetEditorNameText()
        {
            return person?.Name;
        }

        public string SetEditorAgeText()
        {
            return person?.Age.ToString();
        }

        public string SetEditorHeightText()
        {
            return person?.Height.ToString();
        }

        public Gender SetPickerGender()
        {
            return person == null ? Gender.Female : person.Gender;
        }

        public string SetEditorStartWeightText()
        {
            return person?.StartWeight.ToString();
        }

        public string SetEntryPasswordText()
        {
            return person?.Password;
        }

        public Gender GetGender(string genderText)
        {
            switch (genderText)
            {
                case "weiblich":
                case "Female":
                    return Gender.Female;
                case "männlich":
                case "Male":
                    return Gender.Male;
                default:
                    {
                        //error message
                        throw new NotSupportedException();
                    }
            }
        }

        public DateTime SetCurrentDateText()
        {
            return DateTime.Now;
        }
    }
}
