namespace PhotoLibrarizer.Engines.Filters.Models
{
    public enum Destinations
    {
        BaseLibraryWithoutDate,
        BaseLibraryWithDate,
        CameraBasedDirectoryWithoutDate,
        CameraBasedDirectoryWithDate,
    }

    public enum DateDirectories
    {
        Year,
        YearMonth,
        YearMonthDay,
        YearMonthDayHour,
        YearMonthDayHourMinute,
        YearMonthDayHourMinuteSecond,
        YearMonthDayHourMinuteSecondMillisecond,
        YearMonthDayHourMinuteSecondMillisecondSize,
        YearMonthDayHourMinuteSecondMillisecondSizeHash
    }
    
    
    public class CustomPatternNamingModel
    {
        public string Patterns { get; set; }
    }

    public class DirectoryDateNamingModel
    {
        public bool Year { get; set; } = true;
        public bool Month { get; set; } = true;
        public bool Day { get; set; } = true;
        public bool Hour { get; set; } = true;
        public bool Minute { get; set; } = true;
        public bool Second { get; set; } = true;
        public bool Millisecond { get; set; } = true;
        public bool Size { get; set; } = true;
        public bool Hash { get; set; } = true;
    }
    
    public class FileDateNamingModel
    {
        public bool Year { get; set; } = true;
        public bool Month { get; set; } = true;
        public bool Day { get; set; } = true;
        public bool Hour { get; set; } = true;
        public bool Minute { get; set; } = true;
        public bool Second { get; set; } = true;
        public bool Millisecond { get; set; } = true;
        public bool Size { get; set; } = true;
        public bool Hash { get; set; } = true;
    }

    public class DirectoryDateNamingModelBuilder
    {
        private DirectoryDateNamingModel model = new DirectoryDateNamingModel();

        public DirectoryDateNamingModelBuilder WithYear(bool enabled = true)
    {
        model.Year = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithMonth(bool enabled = true)
    {
        model.Month = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithDay(bool enabled = true)
    {
        model.Day = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithHour(bool enabled = true)
    {
        model.Hour = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithMinute(bool enabled = true)
    {
        model.Minute = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithSecond(bool enabled = true)
    {
        model.Second = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithMillisecond(bool enabled = true)
    {
        model.Millisecond = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithSize(bool enabled = true)
    {
        model.Size = enabled;
        return this;
    }

        public DirectoryDateNamingModelBuilder WithHash(bool enabled = true)
    {
        model.Hash = enabled;
        return this;
    }

        public DirectoryDateNamingModel Build()
    {
        return model;
    }
    }


    public static class DateDirectoriesExtensions
    {
        public static DateDirectories WithYear(this DateDirectories self) => DateDirectories.Year;
        public static DateDirectories WithYearMonth(this DateDirectories self) => DateDirectories.YearMonth;
        public static DateDirectories WithYearMonthDay(this DateDirectories self) => DateDirectories.YearMonthDay;
        public static DateDirectories WithYearMonthDayHour(this DateDirectories self) => DateDirectories.YearMonthDayHour;
        public static DateDirectories WithYearMonthDayHourMinute(this DateDirectories self) => DateDirectories.YearMonthDayHourMinute;
        public static DateDirectories WithYearMonthDayHourMinuteSecond(this DateDirectories self) => DateDirectories.YearMonthDayHourMinuteSecond;
        public static DateDirectories WithYearMonthDayHourMinuteSecondMillisecond(this DateDirectories self) => DateDirectories.YearMonthDayHourMinuteSecondMillisecond;
        public static DateDirectories WithYearMonthDayHourMinuteSecondMillisecondSize(this DateDirectories self) => DateDirectories.YearMonthDayHourMinuteSecondMillisecondSize;
        public static DateDirectories WithYearMonthDayHourMinuteSecondMillisecondSizeHash(this DateDirectories self) => DateDirectories.YearMonthDayHourMinuteSecondMillisecondSizeHash;
    }
}