using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Generator {
    class Program {
        static void Main(string[] args) {
            string sourceFolder = @"C:\Git\RW\RimWorld-PawnName-Chinese\Source\EastAsiaNames";
            string destinationFolder = @"C:\Git\RW\RimWorld-PawnName-Chinese\Defs\Names";

            Gen(Path.Combine(sourceFolder, "中国女名.txt"),
                Path.Combine(destinationFolder, "Custom_China_First_Female.xml"),
                "Custom_China_First_Female");

            Gen(Path.Combine(sourceFolder, "中国男名.txt"),
                Path.Combine(destinationFolder, "Custom_China_First_Male.xml"),
                "Custom_China_First_Male");

            Gen(Path.Combine(sourceFolder, "中国姓氏.txt"),
                Path.Combine(destinationFolder, "Custom_China_Last.xml"),
                "Custom_China_Last");

            Gen(Path.Combine(sourceFolder, "日本女名.txt"),
                Path.Combine(destinationFolder, "Custom_Japan_First_Female.xml"),
                "Custom_Japan_First_Female");

            Gen(Path.Combine(sourceFolder, "日本男名.txt"),
                Path.Combine(destinationFolder, "Custom_Japan_First_Male.xml"),
                "Custom_Japan_First_Male");

            Gen(Path.Combine(sourceFolder, "日本姓氏.txt"),
                Path.Combine(destinationFolder, "Custom_Japan_Last.xml"),
                "Custom_Japan_Last");

            Gen(Path.Combine(sourceFolder, "昵称_女性.txt"),
                Path.Combine(destinationFolder, "Custom_Nick_Female.xml"),
                "Custom_Nick_Female");

            Gen(Path.Combine(sourceFolder, "昵称_男性.txt"),
                Path.Combine(destinationFolder, "Custom_Nick_Male.xml"),
                "Custom_Nick_Male");

            Gen(Path.Combine(sourceFolder, "昵称_通用.txt"),
                Path.Combine(destinationFolder, "Custom_Nick_Unsex.xml"),
                "Custom_Nick_Unsex");
        }

        static void Gen(string source, string destination, string defName) {

            XDocument doc = XDocument.Load(destination, LoadOptions.PreserveWhitespace);
            XElement def = null;
            foreach (XElement curDef in doc.Root.Elements()) {
                if (curDef.Element("defName").Value == defName) {
                    def = curDef;
                    break;
                }
            }
            if (def == null) {
                Console.WriteLine("Def no found.");
                return;
            }
            XElement shuffledNames = def.Element("shuffledNames");
            shuffledNames.RemoveAll();
            HashSet<string> lineSet = new HashSet<string>(GenLine(source));
            int quadra = 0;
            foreach (string curLine in lineSet) {
                if (curLine.StartsWith("//")) {
                    shuffledNames.Add("\n      ");
                    shuffledNames.Add(new XComment(curLine));
                    quadra = 0;
                    continue;
                }
                if (quadra == 0)
                    shuffledNames.Add("\n      ");
                shuffledNames.Add(new XElement("li", curLine));
                quadra++;
                if (quadra > 4)
                    quadra = 0;
            }
            shuffledNames.Add("\n    ");
            doc.Save(destination);
        }

        static IEnumerable<string> GenLine(string file) {
            Regex validNameRegex = new Regex("^[\\-\\u4e00-\\u9fff\\uf900-\\ufaff]*$");
            using (StreamReader sr = new StreamReader(file)) {
                while (sr.Peek() >= 0) {
                    string line = sr.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    line = line.Trim();
                    if (line.StartsWith("//")) {
                        yield return line;
                        continue;
                    }
                    string[] names = line.Split(',');
                    foreach (string curName in names) {
                        string result = curName.Trim();
                        if (string.IsNullOrEmpty(result))
                            continue;
                        yield return result;
                        if (!validNameRegex.IsMatch(result))
                            Console.WriteLine(result);
                    }
                }
            }
        }
    }
}
