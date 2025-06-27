# Jogo de Cartas “21” com Sockets UDP

Este projeto foi desenvolvido como Trabalho Prático da disciplina de **Redes de Computadores** do curso de Sistemas de Informação da PUC Minas. A proposta foi implementar o jogo de cartas “21” (Blackjack) utilizando sockets UDP para comunicação entre um servidor e múltiplos clientes.

---

## Informações Gerais

- **Curso:** Sistemas de Informação – PUC Minas  
- **Disciplina:** Redes de Computadores  
- **Semestre:** 3º Período  

---

## Participantes

- Hugo Ferreira Silva 
- Ítalo Ângelo Pimenta Antão  
- Izadora Helena Pedrosa Soares Pereira  

---

## 📁 Estrutura do Projeto

O projeto está organizado em duas pastas principais:
Blackjack/

├── JogoServidor/ → Código-fonte do servidor UDP

├── JogoCliente/ → Código-fonte do cliente UDP

---

## Sobre os Códigos

### JogoServidor

O servidor é responsável por:
- Receber conexões de jogadores via protocolo UDP
- Sortear cartas de 1 a 10 e controlar a pontuação
- Avaliar se o jogador venceu, perdeu ou estourou 21 pontos
- Enviar mensagens de resultado para cada cliente

As mensagens são todas em formato textual, como `CARTA:7`, `RESULTADO:ganhou`, etc.

### JogoCliente

O cliente:
- Envia o nome do jogador ao servidor (`ENTRAR:<nome>`)
- Recebe cartas e decide se quer pedir mais (`PEDIR_CARTA`) ou parar (`PARAR`)
- Exibe a pontuação e o resultado final da rodada no terminal

---

## Como foi testado localmente

Durante os testes:

- O **servidor** foi executado em um terminal com:
  cd JogoServidor
  dotnet run
  
- O cliente foi executado em outro terminal com:
  cd JogoCliente
  dotnet run
  
A comunicação foi feita usando localhost (127.0.0.1) e porta 5000.
No código do cliente, a linha responsável pela conexão está assim:

- IPEndPoint servidorEP = new IPEndPoint(IPAddress.Loopback, 5000);

# Como rodar em dois computadores na mesma rede

## 1. Descubra o IP do servidor
No computador que executará o servidor, abra o terminal e digite:

ipconfig

Copie o Endereço IPv4 (exemplo: 192.168.0.105)

## 2. Altere o IP no cliente
No arquivo Program.cs do cliente, substitua:

IPEndPoint servidorEP = new IPEndPoint(IPAddress.Loopback, 5000);

por:

IPEndPoint servidorEP = new IPEndPoint(IPAddress.Parse("IP_DA_SUA_MÁQUINA"), 5000);

## 3. Libere a porta no firewall (porta 5000, protocolo UDP)
No computador servidor:

Abra o Firewall do Windows

Vá em Regras de Entrada > Nova Regra

Tipo: Porta, protocolo: UDP

Porta: 5000

Permitir a conexão

## Protocolo de Comunicação

Comando	            Origem → Destino	    Descrição
ENTRAR:<nome>	      Cliente → Servidor	  Jogador entra no jogo
PEDIR_CARTA	        Cliente → Servidor	  Solicita uma nova carta
PARAR	              Cliente → Servidor	  Jogador encerra sua vez
CARTA:<valor>	      Servidor → Cliente	  Envia carta sorteada
RESULTADO:<status>	Servidor → Cliente	  Resultado da rodada (ganhou/perdeu)
MENSAGEM:<texto>	  Servidor → Cliente	  Mensagens de boas-vindas ou sistema

## Observações finais
O projeto foi testado com sucesso em rede local com dois terminais e também em dois computadores diferentes.

A comunicação via sockets UDP funciona com mensagens simples e diretas.







