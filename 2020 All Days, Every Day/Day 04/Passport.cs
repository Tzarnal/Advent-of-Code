using System;
using System.Collections.Generic;
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
            if (PassportFields.ContainsKey("byr"))
            {
                byr = int.Parse(PassportFields["byr"]);
            }

            if (PassportFields.ContainsKey("iyr"))
            {
                iyr = int.Parse(PassportFields["iyr"]);
            }

            if (PassportFields.ContainsKey("eyr"))
            {
                eyr = int.Parse(PassportFields["eyr"]);
            }

            if (PassportFields.ContainsKey("hgt"))
            {
                hgt = PassportFields["hgt"];
                if (hgt.Contains("cm"))
                {
                    hgtUnit = "cm";
                    hgtNumber = int.Parse(hgt.Replace("cm", ""));
                }

                if (hgt.Contains("in"))
                {
                    hgtUnit = "in";
                    hgtNumber = int.Parse(hgt.Replace("in", ""));
                }
            }

            if (PassportFields.ContainsKey("hcl"))
            {
                hcl = PassportFields["hcl"];
            }

            if (PassportFields.ContainsKey("ecl"))
            {
                ecl = PassportFields["ecl"];
            }

            if (PassportFields.ContainsKey("pid"))
            {
                pid = PassportFields["pid"];
            }

            if (PassportFields.ContainsKey("cid"))
            {
                cid = PassportFields["cid"];
            }
        }

        public bool Valid()
        {
            if (ValidBYR() && ValidIYR() && ValidEYR() && ValidHGT() && ValidHCL() && ValidECL() && validPID() && validCID())
                return true;

            return false;
        }

        public bool ValidWithoutCid()
        {
            if (ValidBYR() && ValidIYR() && ValidEYR() && ValidHGT() && ValidHCL() && ValidECL() && validPID())
                return true;

            return false;
        }

        public bool ValidBYR()
        {
            if (byr == 0)
                return false;

            //four digit guard
            if (byr < 1000)
                return false;
            //four digit guard
            if (byr > 9999)
                return false;

            if (byr > 2002)
                return false;

            if (byr < 1920)
                return false;

            return true;
        }

        public bool ValidIYR()
        {
            if (iyr == 0)
                return false;

            //four digit guard
            if (iyr < 1000)
                return false;
            //four digit guard
            if (iyr > 9999)
                return false;

            if (iyr > 2020)
                return false;

            if (iyr < 2010)
                return false;

            return true;
        }

        public bool ValidEYR()
        {
            if (eyr == 0)
                return false;

            //four digit guard
            if (eyr < 1000)
                return false;
            //four digit guard
            if (eyr > 9999)
                return false;

            if (eyr > 2030)
                return false;

            if (eyr < 2020)
                return false;

            return true;
        }

        public bool ValidHGT()
        {
            if (string.IsNullOrWhiteSpace(hgt))
            {
                return false;
            }

            if (hgtUnit == "cm")
            {
                if (hgtNumber < 150 || hgtNumber > 193)
                    return false;
            }
            else if (hgtUnit == "in")
            {
                if (hgtNumber < 59 || hgtNumber > 76)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool ValidHCL()
        {
            if (string.IsNullOrWhiteSpace(hcl))
            {
                return false;
            }

            var hclRegex = @"#[\d\w]{6}";

            if (hcl[0] != '#')
                return false;

            if (hcl.Length != 7)
                return false;

            var hclMatch = Regex.Match(hcl, hclRegex);
            if (!hclMatch.Success)
                return false;

            return true;
        }

        public bool ValidECL()
        {
            if (string.IsNullOrWhiteSpace(ecl))
            {
                return false;
            }

            var validECLOptions = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            foreach (var validOption in validECLOptions)
            {
                if (ecl == validOption)
                    return true;
            }

            return false;
        }

        public bool validPID()
        {
            if (string.IsNullOrWhiteSpace(pid))
            {
                return false;
            }

            if (pid.Length == 9 && int.TryParse(pid, out _))
            {
                return true;
            }

            return false;
        }

        public bool validCID()
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return false;
            }

            return true;
        }
    }
}