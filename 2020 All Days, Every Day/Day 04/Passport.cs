using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Day_04
{
    public class Passport
    {
        public int byr;//(Birth Year)

        public int iyr;//(Issue Year)
        public int eyr;//(Expiration Year)

        public string hgt;//(Height)
        public int hgtNumber;
        public string hgtUnit;

        public string hcl;//(Hair Color)
        public string ecl;//(Eye Color)

        public string pid;//(Passport ID)

        public string cid;//private (Country ID)

        public Passport()
        {
        }

        public Passport(Dictionary<string, string> PassportFields)
        {
            foreach (var field in PassportFields)
            {
                switch (field.Key)
                {
                    case "byr":
                        byr = int.Parse(field.Value);
                        break;

                    case "iyr":
                        iyr = int.Parse(field.Value);
                        break;

                    case "eyr":
                        eyr = int.Parse(field.Value);
                        break;

                    case "hgt":
                        hgt = field.Value;
                        hgtUnit = field.Value.Substring(field.Value.Length - 2);
                        hgtNumber = int.Parse(field.Value.Replace("cm", "").Replace("in", ""));
                        break;

                    case "hcl":
                        hcl = field.Value;
                        break;

                    case "ecl":
                        ecl = field.Value;
                        break;

                    case "pid":
                        pid = field.Value;
                        break;

                    case "cid":
                        cid = field.Value;
                        break;
                }
            }
        }

        public bool Valid()
        {
            return ValidBYR && ValidIYR && ValidEYR && ValidHGT
                && ValidHCL && ValidECL && ValidPID && ValidCID;
        }

        public bool ValidWithoutCid()
        {
            return ValidBYR && ValidIYR && ValidEYR && ValidHGT
                && ValidHCL && ValidECL && ValidPID;
        }

        public bool ValidBYR => 1920 <= byr && byr <= 2002;
        public bool ValidIYR => 2010 <= iyr && iyr <= 2020;
        public bool ValidEYR => 2020 <= eyr && eyr <= 2030;

        public bool ValidHGT => (hgtUnit == "cm" && (150 <= hgtNumber && hgtNumber <= 193)) ||
                                (hgtUnit == "in" && (59 <= hgtNumber && hgtNumber <= 76));

        public bool ValidHCL => !string.IsNullOrWhiteSpace(hcl)
                                    && Regex.Match(hcl, @"#[\d\w]{6}").Success;

        public bool ValidECL => new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(ecl);
        public bool ValidPID => !string.IsNullOrWhiteSpace(pid) && pid.Length == 9 && int.TryParse(pid, out _);
        public bool ValidCID => !string.IsNullOrWhiteSpace(cid);
    }
}