using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        UdpClient servidor = new UdpClient(5000);
        Console.WriteLine("Servidor iniciado na porta 5000...");

        Hashtable jogadores = new Hashtable();
        Hashtable pontuacoes = new Hashtable();
        Random random = new Random();

        while (true)
        {
            IPEndPoint ipCliente = new IPEndPoint(IPAddress.Any, 0);
            byte[] dados = servidor.Receive(ref ipCliente);
            string mensagem = Encoding.UTF8.GetString(dados);
            Console.WriteLine($"Recebido de {ipCliente}: {mensagem}");

            if (mensagem.StartsWith("ENTRAR:"))
            {
                string nome = mensagem.Split(':')[1];
                jogadores[ipCliente] = nome;
                pontuacoes[ipCliente] = 0;
                Enviar(servidor, ipCliente, $"MENSAGEM:Bem-vindo, {nome}!");
                EnviarCarta(servidor, ipCliente, pontuacoes, random);
            }
            else if (mensagem == "PEDIR_CARTA")
            {
                EnviarCarta(servidor, ipCliente, pontuacoes, random);
            }
            else if (mensagem == "PARAR")
            {
                int pontos = (int)pontuacoes[ipCliente];
                string resultado = pontos > 21 ? "perdeu" : "ganhou";
                Enviar(servidor, ipCliente, $"RESULTADO:{resultado}");
            }
        }
    }

    static void EnviarCarta(UdpClient servidor, IPEndPoint cliente, Hashtable pontuacoes, Random random)
    {
        int carta = random.Next(1, 11); // Cartas de 1 a 10
        int atual = (int)pontuacoes[cliente];
        atual += carta;
        pontuacoes[cliente] = atual;

        Enviar(servidor, cliente, $"CARTA:{carta}");
        if (atual > 21)
        {
            Enviar(servidor, cliente, "RESULTADO:perdeu");
        }
    }

    static void Enviar(UdpClient servidor, IPEndPoint cliente, string msg)
    {
        byte[] dados = Encoding.UTF8.GetBytes(msg);
        servidor.Send(dados, dados.Length, cliente);
    }
}
