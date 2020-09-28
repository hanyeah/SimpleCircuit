﻿using SimpleCircuit;
using SimpleCircuit.Components;
using SimpleCircuit.Parser;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var lexer = new SimpleCircuitLexer("G <ud> n.R.p");
            var token = lexer.Next(out string content);
            while (token != TokenType.EndOfContent)
            {
                Console.WriteLine($"{content} ({token})");
                token = lexer.Next(out content);
            }

            /*
            var p = new SimpleCircuitParser();
            SimpleCircuit.Functions.Minimizer.LogInfo = true;
            var ckt = p.Parse(@"G d R");
            var doc = ckt.Render();

            using var sw = new StringWriter();
            using (var xml = XmlWriter.Create(sw, new XmlWriterSettings { OmitXmlDeclaration = true }))
                doc.WriteTo(xml);
            Console.WriteLine(sw.ToString());

            if (File.Exists("tmp.html"))
                File.Delete("tmp.html");
            using (var fw = new StreamWriter(File.OpenWrite("tmp.html")))
            {
                fw.WriteLine("<html>");
                fw.WriteLine("<head>");
                fw.WriteLine("<style>");
                fw.WriteLine(@"
                path, polyline, line, circle {
                    stroke: black;
                    stroke-width: 0.5pt;
                    fill: transparent;
                    stroke-linecap: round;
                }
                .point circle {
                    fill: black;
                }
                .plane {
                    stroke-width: 1pt;
                }
                text {
                    font: 4pt Tahoma, Verdana, Segoe, sans-serif;
                }");
                fw.WriteLine("</style>");
                fw.WriteLine("</head>");
                fw.WriteLine("<body>");
                fw.WriteLine(sw.ToString());
                fw.WriteLine("</body>");
                fw.WriteLine("</html>");
            }
            Process.Start(@"""C:\Program Files (x86)\Google\Chrome\Application\chrome.exe""", "\"" + Path.Combine(Directory.GetCurrentDirectory(), "tmp.html") + "\"");
            */
        }
    }
}
