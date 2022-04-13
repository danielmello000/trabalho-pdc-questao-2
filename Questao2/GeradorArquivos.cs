using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RandomNameGeneratorLibrary;

namespace Questao2
{
    public static class GeradorArquivos
    {
        public static void Write()
        {
            var quantidadeLinhas = 100000;
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var random = new Random();
            var cursos = new List<string> { "CIÊNCIA DA COMPUTAÇÃO", "ADMINISTRAÇÃO", "MEDICINA", "EDUCAÇÃO FÍSICA", "NUTRIÇÃO",
                                            "ENGENHARIA DA COMPUTAÇÃO", "DIREITO", "PEDAGOGIA", "PSICOLOGIA", "VETERINÁRIA",
                                            "SISTEMAS DE INFORMAÇÃO", "PRODUÇÃO DE CACHAÇA", "TEATRO", "DESIGN DE PRODUTO", "ODONTOLOGIA"};
            var conclusao = new List<string> { "CURSANDO", "CONCLUIDO" };

            foreach (var curso in cursos)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, $"alunos {curso.ToLower()}.txt")))
                {
                    for (int i = 0; i < quantidadeLinhas; i++)
                    {
                        var personGenerator = new PersonNameGenerator();
                        var name = personGenerator.GenerateRandomFirstAndLastName();

                        outputFile.WriteLine($"{random.Next(999999999)} {name} {curso} {conclusao.ElementAt(random.Next(conclusao.Count))}");
                    }
                }
            }
        }
    }
}
