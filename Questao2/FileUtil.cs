using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using RandomNameGeneratorLibrary;
using System.Collections.Concurrent;

namespace Questao2
{
    public static class FileUtil
    {
        public static List<string> Cursos { get; set; } = new List<string> { "CIÊNCIA DA COMPUTAÇÃO", "ADMINISTRAÇÃO", "MEDICINA", "EDUCAÇÃO FÍSICA", "NUTRIÇÃO",
                                            "ENGENHARIA DA COMPUTAÇÃO", "DIREITO", "PEDAGOGIA", "PSICOLOGIA", "VETERINÁRIA",
                                            "SISTEMAS DE INFORMAÇÃO", "PRODUÇÃO DE CACHAÇA", "TEATRO", "DESIGN DE PRODUTO", "ODONTOLOGIA"};
        public static List<string> Flags { get; set; } = new List<string> { "CURSANDO", "CONCLUIDO" };

        /// <summary>
        /// Obtem o caminho do projeto
        /// </summary>
        /// <returns></returns>
        private static string GetPathProjeto()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }

        /// <summary>
        /// Gera um arquivo para cada curso de forma concorrente
        /// </summary>
        public static void GerarArquivosCursos()
        {
            var tasks = new List<Task>();

            // Dispara uma thread para gerar cada arquivo de curso
            foreach (var curso in Cursos)
            {
                tasks.Add(Task.Run(() => GerarArquivoCurso(curso)));
            }

            // Aguarda todas as threads terminarem
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Gera um arquivo de alunos de um curso
        /// </summary>
        /// <param name="curso">Nome do curso a ser escrito no arquivo</param>
        public static void GerarArquivoCurso(string curso)
        {
            var path = GetPathProjeto();
            var quantidadeLinhas = 100000;
            var random = new Random();

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, $"alunos {curso.ToLower()}.txt")))
            {
                for (int i = 0; i < quantidadeLinhas; i++)
                {
                    var personGenerator = new PersonNameGenerator();
                    var name = personGenerator.GenerateRandomFirstAndLastName();

                    outputFile.WriteLine($"{random.Next(999999999)} {name} {curso} {Flags.ElementAt(random.Next(Flags.Count))}");
                }
            }
        }

        /// <summary>
        /// Obtem caminhos completos dos arquivos de curso
        /// </summary>
        /// <returns>Lista contendo os caminhos dos arquivos de curso</returns>
        public static List<string> GetArquivosCursos()
        {
            var files = Directory
                .EnumerateFiles(GetPathProjeto(), "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => "txt".Equals(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()))
                .Where( s => Path.GetFileName(s).StartsWith("alunos"))
                .ToList();

            return files;
        }

        /// <summary>
        /// Lê um arquivo e adiciona o aluno na lista de formandos
        /// </summary>
        /// <param name="fileName">Nome do arquivo a ser lido</param>
        /// <param name="formandos">Lista de formandos a adicionar os alunos</param>
        public static void LerArquivo(string fileName, ConcurrentBag<string> formandos)
        {
            foreach (string line in File.ReadLines(fileName))
            {
                var flag = line.Split().Last();
                if(flag.Equals("CONCLUIDO", StringComparison.OrdinalIgnoreCase))
                {
                    formandos.Add(line);
                }
            }
        }

        /// <summary>
        /// Grava um arquivo com os alunos da lista
        /// </summary>
        /// <param name="fileName">Nome do arquivo a ser gravado</param>
        /// <param name="formandos">Lista contendo os formandos</param>
        public static void GravarArquivo(string fileName, ConcurrentBag<string> formandos)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(GetPathProjeto(), $"{fileName}.txt")))
            {
                while(!formandos.IsEmpty)
                {
                    string item;
                    if (formandos.TryTake(out item))
                    {
                        outputFile.WriteLine(item);
                    }
                }
            }
        }
    }
}
