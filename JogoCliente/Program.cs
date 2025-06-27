using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        UdpClient cliente = new UdpClient();
        IPEndPoint servidorEP = new IPEndPoint(IPAddress.Parse("192.168.100.252"), 5000);

        Console.Write("Digite seu nome: ");
        string nome = Console.ReadLine();

        Enviar(cliente, servidorEP, $"ENTRAR:{nome}");

        bool jogando = true;
        while (jogando)
        {
            IPEndPoint remotoEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] dados = cliente.Receive(ref remotoEP);
            string resposta = Encoding.UTF8.GetString(dados);
            Console.WriteLine($"Servidor: {resposta}");

            if (resposta.StartsWith("RESULTADO"))
            {
                jogando = false;
            }
            else if (resposta.StartsWith("CARTA"))
            {
                Console.Write("Deseja pedir outra carta? (s/n): ");
                string escolha = Console.ReadLine();
                if (escolha.ToLower() == "s")
                {
                    Enviar(cliente, servidorEP, "PEDIR_CARTA");
                }
                else
                {
                    Enviar(cliente, servidorEP, "PARAR");
                }
            }
        }

        Console.WriteLine("Fim do jogo.");
    }

    static void Enviar(UdpClient cliente, IPEndPoint destino, string msg)
    {
        byte[] dados = Encoding.UTF8.GetBytes(msg);
        cliente.Send(dados, dados.Length, destino);
    }
}
