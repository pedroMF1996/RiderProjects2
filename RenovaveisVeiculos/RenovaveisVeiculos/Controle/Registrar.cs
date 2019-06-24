using System;
using System.Collections.Generic;
using System.IO;
using RenovaveisVeiculos.Modelo;

namespace RenovaveisVeiculos.Controle
{
    public class Registrar
    {
        public static string SourcePath { get; set; }
        public static StreamReader Leitor { get; set; }
        public static StreamWriter Escritor { get; set; }

        public Registrar(string path)
        {
            SourcePath = path;
        }

        private static StreamReader SetReader() => new StreamReader(SourcePath);
        private static StreamWriter SetWriter() => new StreamWriter(SourcePath);
        
        private static void DisposeReader() => Leitor.Dispose();
        private static void DisposeWriter() => Escritor.Dispose();
        
        //Encontra
        public bool FindById(int IdTransacao)
        {   
            throw new ApplicationException("Precisa ser implementado");
        }
        
        //Inserir
        public int Insert()
        {
            throw new ApplicationException("Precisa ser implementado");
        }
        
        //Alterar
        public int Update()
        {
            throw new ApplicationException("Precisa ser implementado");
        }
        
        //Remover
        public int Remove()
        {
            throw new ApplicationException("Precisa ser implementado");
        }
        
        //Listar
        public List<Transacao> List(string parameter)
        {
            List<Transacao> transList = new List<Transacao>();
            Leitor = SetReader();
            while (!Leitor.EndOfStream)
            {
                var conteudo = Leitor.ReadLineAsync().ToString().Split('\\');
                
            }
            throw new ApplicationException("Precisa ser implementado");
        }
    }
}